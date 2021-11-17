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
    [RoutePrefix("api/products")]
    public partial class ProductController : HealthRestApiController
    {
        public ProductController(IFoundation foundation)
            : base(foundation, "Product")
        {
        }

        [HttpGet]
        [Route("{product_id}")]
        public object GetById(Guid product_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Product result = this.API.Index.Products.GetById(product_id);
                if (result == null)
                {
                    return Http404("Product");
                }

                

                return base.Http200(new ItemResult<sdk.Product>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("")]
        public object Find(int skip = 0, int take = 10, string order_by = "", bool descending = false, string keyword = "", Guid? brand_id = null)
        {
            return base.ExecuteFunction<object>("Find", delegate()
            {
                
                ListResult<sdk.Product> result = this.API.Index.Products.Find(skip, take, keyword, order_by, descending, brand_id);
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
                
                
                ListResult<sdk.Product> result = this.API.Index.Products.GetByBrandId(brand_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Product product)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(product, "Product");

                dm.Product insert = product.ToDomainModel();

                
                insert = this.API.Direct.Products.Insert(insert);
                

                
                sdk.Product result = this.API.Index.Products.GetById(insert.product_id);

                return base.Http201(new ItemResult<sdk.Product>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/product/{0}", product.product_id));

            });

        }


        [HttpPut]
        [Route("{product_id}")]
        public object Update(Guid product_id, sdk.Product product)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(product, "Product");
                this.ValidateRouteMatch(product_id, product.product_id, "Product");

                product.product_id = product_id;
                dm.Product update = product.ToDomainModel();


                update = this.API.Direct.Products.Update(update);
                
                
                sdk.Product existing = this.API.Index.Products.GetById(update.product_id);
                
                
                return base.Http200(new ItemResult<sdk.Product>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{product_id}")]
        public object Delete(Guid product_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Product delete = this.API.Direct.Products.GetById(product_id);
                
                
                this.API.Direct.Products.Delete(product_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = product_id.ToString()
                });
            });
        }

    }
}

