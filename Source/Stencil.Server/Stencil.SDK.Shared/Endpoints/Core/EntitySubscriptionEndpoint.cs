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
    public partial class EntitySubscriptionEndpoint : EndpointBase
    {
        public EntitySubscriptionEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<EntitySubscription>> GetEntitySubscriptionAsync(Guid subscription_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "entitysubscriptions/{subscription_id}";
            request.AddUrlSegment("subscription_id", subscription_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<EntitySubscription>>(request);
        }
        
        
        public Task<ListResult<EntitySubscription>> GetEntitySubscriptionByBrandIdAsync(Guid brand_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "entitysubscriptions/by_brandid/{brand_id}";
            request.AddUrlSegment("brand_id", brand_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<EntitySubscription>>(request);
        }
        

        public Task<ItemResult<EntitySubscription>> CreateEntitySubscriptionAsync(EntitySubscription entitysubscription)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "entitysubscriptions";
            request.AddJsonBody(entitysubscription);
            return this.Sdk.ExecuteAsync<ItemResult<EntitySubscription>>(request);
        }

        public Task<ItemResult<EntitySubscription>> UpdateEntitySubscriptionAsync(Guid subscription_id, EntitySubscription entitysubscription)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "entitysubscriptions/{subscription_id}";
            request.AddUrlSegment("subscription_id", subscription_id.ToString());
            request.AddJsonBody(entitysubscription);
            return this.Sdk.ExecuteAsync<ItemResult<EntitySubscription>>(request);
        }

        

        public Task<ActionResult> DeleteEntitySubscriptionAsync(Guid subscription_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "entitysubscriptions/{subscription_id}";
            request.AddUrlSegment("subscription_id", subscription_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
