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
    public partial class BrandEndpoint : EndpointBase
    {
        public BrandEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Brand>> GetBrandAsync(Guid brand_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "brands/{brand_id}";
            request.AddUrlSegment("brand_id", brand_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Brand>>(request);
        }
        
        public Task<ListResult<Brand>> Find(int skip = 0, int take = 10, string keyword = "", string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "brands";
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            request.AddParameter("keyword", keyword);
            
            
            return this.Sdk.ExecuteAsync<ListResult<Brand>>(request);
        }

        public Task<ItemResult<Brand>> CreateBrandAsync(Brand brand)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "brands";
            request.AddJsonBody(brand);
            return this.Sdk.ExecuteAsync<ItemResult<Brand>>(request);
        }

        public Task<ItemResult<Brand>> UpdateBrandAsync(Guid brand_id, Brand brand)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "brands/{brand_id}";
            request.AddUrlSegment("brand_id", brand_id.ToString());
            request.AddJsonBody(brand);
            return this.Sdk.ExecuteAsync<ItemResult<Brand>>(request);
        }

        

        public Task<ActionResult> DeleteBrandAsync(Guid brand_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "brands/{brand_id}";
            request.AddUrlSegment("brand_id", brand_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
