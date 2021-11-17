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
    [RoutePrefix("api/brands")]
    public partial class BrandController : HealthRestApiController
    {
        public BrandController(IFoundation foundation)
            : base(foundation, "Brand")
        {
        }

        [HttpGet]
        [Route("{brand_id}")]
        public object GetById(Guid brand_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Brand result = this.API.Index.Brands.GetById(brand_id);
                if (result == null)
                {
                    return Http404("Brand");
                }

                

                return base.Http200(new ItemResult<sdk.Brand>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("")]
        public object Find(int skip = 0, int take = 10, string order_by = "", bool descending = false, string keyword = "")
        {
            return base.ExecuteFunction<object>("Find", delegate()
            {
                
                ListResult<sdk.Brand> result = this.API.Index.Brands.Find(skip, take, keyword, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Brand brand)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(brand, "Brand");

                dm.Brand insert = brand.ToDomainModel();

                
                insert = this.API.Direct.Brands.Insert(insert);
                

                
                sdk.Brand result = this.API.Index.Brands.GetById(insert.brand_id);

                return base.Http201(new ItemResult<sdk.Brand>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/brand/{0}", brand.brand_id));

            });

        }


        [HttpPut]
        [Route("{brand_id}")]
        public object Update(Guid brand_id, sdk.Brand brand)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(brand, "Brand");
                this.ValidateRouteMatch(brand_id, brand.brand_id, "Brand");

                brand.brand_id = brand_id;
                dm.Brand update = brand.ToDomainModel();


                update = this.API.Direct.Brands.Update(update);
                
                
                sdk.Brand existing = this.API.Index.Brands.GetById(update.brand_id);
                
                
                return base.Http200(new ItemResult<sdk.Brand>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{brand_id}")]
        public object Delete(Guid brand_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Brand delete = this.API.Direct.Brands.GetById(brand_id);
                
                
                this.API.Direct.Brands.Delete(brand_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = brand_id.ToString()
                });
            });
        }

    }
}

