#if WINDOWS_PHONE_APP
using RestSharp.Portable;
#else
using RestSharp;
#endif
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stencil.SDK.Models;

namespace Stencil.SDK.Endpoints
{
    public partial class ProductEndpoint : EndpointBase
    {
        public ProductEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Product>> GetProductAsync(Guid product_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "products/{product_id}";
            request.AddUrlSegment("product_id", product_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Product>>(request);
        }
        
        public Task<ListResult<Product>> Find(int skip = 0, int take = 10, string keyword = "", string order_by = "", bool descending = false, Guid? brand_id = null)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "products";
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            request.AddParameter("keyword", keyword);
            request.AddParameter("brand_id", brand_id);
            
            
            return this.Sdk.ExecuteAsync<ListResult<Product>>(request);
        }
        public Task<ListResult<Product>> GetProductByBrandIdAsync(Guid brand_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "products/by_brandid/{brand_id}";
            request.AddUrlSegment("brand_id", brand_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Product>>(request);
        }
        

        public Task<ItemResult<Product>> CreateProductAsync(Product product)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "products";
            request.AddJsonBody(product);
            return this.Sdk.ExecuteAsync<ItemResult<Product>>(request);
        }

        public Task<ItemResult<Product>> UpdateProductAsync(Guid product_id, Product product)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "products/{product_id}";
            request.AddUrlSegment("product_id", product_id.ToString());
            request.AddJsonBody(product);
            return this.Sdk.ExecuteAsync<ItemResult<Product>>(request);
        }

        

        public Task<ActionResult> DeleteProductAsync(Guid product_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "products/{product_id}";
            request.AddUrlSegment("product_id", product_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
