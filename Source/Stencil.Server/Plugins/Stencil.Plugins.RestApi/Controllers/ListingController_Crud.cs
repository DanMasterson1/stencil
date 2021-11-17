using Codeable.Foundation.Common;
using Codeable.Foundation.Core;
using System;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using sdk = Stencil.SDK.Models;
using dm = Stencil.Domain;
using Stencil.Primary;
using Stencil.SDK;
using Stencil.Web.Controllers;
using Stencil.Web.Security;

namespace Stencil.Plugins.RestAPI.Controllers
{
    [ApiKeyHttpAuthorize]
    [RoutePrefix("api/listings")]
    public partial class ListingController : HealthRestApiController
    {
        public ListingController(IFoundation foundation)
            : base(foundation, "Listing")
        {
        }

        [HttpGet]
        [Route("{listing_id}")]
        public object GetById(Guid listing_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Listing result = this.API.Index.Listings.GetById(listing_id);
                if (result == null)
                {
                    return Http404("Listing");
                }

                

                return base.Http200(new ItemResult<sdk.Listing>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("")]
        public object Find(int skip = 0, int take = 10, string order_by = "", bool descending = false, string keyword = "", Guid? brand_id = null, Guid? product_id = null, Guid? promotion_id = null)
        {
            return base.ExecuteFunction<object>("Find", delegate()
            {
                
                ListResult<sdk.Listing> result = this.API.Index.Listings.Find(skip, take, keyword, order_by, descending, brand_id, product_id, promotion_id);
                result.success = true;
                return base.Http200(result);
            });
        }
        [HttpGet]
        [Route("by_brandid/{brand_id}")]
        public object GetByBrandId(Guid brand_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByBrandId", delegate ()
            {
                
                
                ListResult<sdk.Listing> result = this.API.Index.Listings.GetByBrandId(brand_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        [HttpGet]
        [Route("by_productid/{product_id}")]
        public object GetByProductId(Guid product_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByProductId", delegate ()
            {
                
                
                ListResult<sdk.Listing> result = this.API.Index.Listings.GetByProductId(product_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        [HttpGet]
        [Route("by_promotionid/{promotion_id}")]
        public object GetByPromotionId(Guid promotion_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByPromotionId", delegate ()
            {
                
                
                ListResult<sdk.Listing> result = this.API.Index.Listings.GetByPromotionId(promotion_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Listing listing)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(listing, "Listing");

                dm.Listing insert = listing.ToDomainModel();

                
                insert = this.API.Direct.Listings.Insert(insert);
                

                
                sdk.Listing result = this.API.Index.Listings.GetById(insert.listing_id);

                return base.Http201(new ItemResult<sdk.Listing>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/listing/{0}", listing.listing_id));

            });

        }


        [HttpPut]
        [Route("{listing_id}")]
        public object Update(Guid listing_id, sdk.Listing listing)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(listing, "Listing");
                this.ValidateRouteMatch(listing_id, listing.listing_id, "Listing");

                listing.listing_id = listing_id;
                dm.Listing update = listing.ToDomainModel();


                update = this.API.Direct.Listings.Update(update);
                
                
                sdk.Listing existing = this.API.Index.Listings.GetById(update.listing_id);
                
                
                return base.Http200(new ItemResult<sdk.Listing>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{listing_id}")]
        public object Delete(Guid listing_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Listing delete = this.API.Direct.Listings.GetById(listing_id);
                
                
                this.API.Direct.Listings.Delete(listing_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = listing_id.ToString()
                });
            });
        }

    }
}

