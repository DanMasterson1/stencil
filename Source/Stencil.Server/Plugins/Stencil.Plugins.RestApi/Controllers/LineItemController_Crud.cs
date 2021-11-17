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
    [RoutePrefix("api/lineitems")]
    public partial class LineItemController : HealthRestApiController
    {
        public LineItemController(IFoundation foundation)
            : base(foundation, "LineItem")
        {
        }

        [HttpGet]
        [Route("{lineitem_id}")]
        public object GetById(Guid lineitem_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.LineItem result = this.API.Index.LineItems.GetById(lineitem_id);
                if (result == null)
                {
                    return Http404("LineItem");
                }

                

                return base.Http200(new ItemResult<sdk.LineItem>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("by_orderid/{order_id}")]
        public object GetByOrderId(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByOrderId", delegate ()
            {
                
                
                ListResult<sdk.LineItem> result = this.API.Index.LineItems.GetByOrderId(order_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        [HttpGet]
        [Route("by_listingid/{listing_id}")]
        public object GetByListingId(Guid listing_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByListingId", delegate ()
            {
                
                
                ListResult<sdk.LineItem> result = this.API.Index.LineItems.GetByListingId(listing_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.LineItem lineitem)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(lineitem, "LineItem");

                dm.LineItem insert = lineitem.ToDomainModel();

                
                insert = this.API.Direct.LineItems.Insert(insert);
                

                
                sdk.LineItem result = this.API.Index.LineItems.GetById(insert.lineitem_id);

                return base.Http201(new ItemResult<sdk.LineItem>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/lineitem/{0}", lineitem.lineitem_id));

            });

        }


        [HttpPut]
        [Route("{lineitem_id}")]
        public object Update(Guid lineitem_id, sdk.LineItem lineitem)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(lineitem, "LineItem");
                this.ValidateRouteMatch(lineitem_id, lineitem.lineitem_id, "LineItem");

                lineitem.lineitem_id = lineitem_id;
                dm.LineItem update = lineitem.ToDomainModel();


                update = this.API.Direct.LineItems.Update(update);
                
                
                sdk.LineItem existing = this.API.Index.LineItems.GetById(update.lineitem_id);
                
                
                return base.Http200(new ItemResult<sdk.LineItem>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{lineitem_id}")]
        public object Delete(Guid lineitem_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.LineItem delete = this.API.Direct.LineItems.GetById(lineitem_id);
                
                
                this.API.Direct.LineItems.Delete(lineitem_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = lineitem_id.ToString()
                });
            });
        }

    }
}

