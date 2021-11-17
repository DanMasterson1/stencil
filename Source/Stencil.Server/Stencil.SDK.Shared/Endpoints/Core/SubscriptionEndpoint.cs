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
    public partial class SubscriptionEndpoint : EndpointBase
    {
        public SubscriptionEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Subscription>> GetSubscriptionAsync(Guid subscription_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "subscriptions/{subscription_id}";
            request.AddUrlSegment("subscription_id", subscription_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Subscription>>(request);
        }
        
        
        public Task<ListResult<Subscription>> GetSubscriptionByBrandIdAsync(Guid brand_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "subscriptions/by_brandid/{brand_id}";
            request.AddUrlSegment("brand_id", brand_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Subscription>>(request);
        }
        
        public Task<ListResult<Subscription>> GetSubscriptionByProductIdAsync(Guid product_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "subscriptions/by_productid/{product_id}";
            request.AddUrlSegment("product_id", product_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Subscription>>(request);
        }
        

        public Task<ItemResult<Subscription>> CreateSubscriptionAsync(Subscription subscription)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "subscriptions";
            request.AddJsonBody(subscription);
            return this.Sdk.ExecuteAsync<ItemResult<Subscription>>(request);
        }

        public Task<ItemResult<Subscription>> UpdateSubscriptionAsync(Guid subscription_id, Subscription subscription)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "subscriptions/{subscription_id}";
            request.AddUrlSegment("subscription_id", subscription_id.ToString());
            request.AddJsonBody(subscription);
            return this.Sdk.ExecuteAsync<ItemResult<Subscription>>(request);
        }

        

        public Task<ActionResult> DeleteSubscriptionAsync(Guid subscription_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "subscriptions/{subscription_id}";
            request.AddUrlSegment("subscription_id", subscription_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
