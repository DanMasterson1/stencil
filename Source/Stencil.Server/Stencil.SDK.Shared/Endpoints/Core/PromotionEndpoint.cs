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
    public partial class PromotionEndpoint : EndpointBase
    {
        public PromotionEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Promotion>> GetPromotionAsync(Guid promotion_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "promotions/{promotion_id}";
            request.AddUrlSegment("promotion_id", promotion_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Promotion>>(request);
        }
        
        

        public Task<ItemResult<Promotion>> CreatePromotionAsync(Promotion promotion)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "promotions";
            request.AddJsonBody(promotion);
            return this.Sdk.ExecuteAsync<ItemResult<Promotion>>(request);
        }

        public Task<ItemResult<Promotion>> UpdatePromotionAsync(Guid promotion_id, Promotion promotion)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "promotions/{promotion_id}";
            request.AddUrlSegment("promotion_id", promotion_id.ToString());
            request.AddJsonBody(promotion);
            return this.Sdk.ExecuteAsync<ItemResult<Promotion>>(request);
        }

        

        public Task<ActionResult> DeletePromotionAsync(Guid promotion_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "promotions/{promotion_id}";
            request.AddUrlSegment("promotion_id", promotion_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
