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
    public partial class PaymentEndpoint : EndpointBase
    {
        public PaymentEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Payment>> GetPaymentAsync(Guid payment_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "payments/{payment_id}";
            request.AddUrlSegment("payment_id", payment_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Payment>>(request);
        }
        
        
        public Task<ListResult<Payment>> GetPaymentByOrderIdAsync(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "payments/by_orderid/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Payment>>(request);
        }
        

        public Task<ItemResult<Payment>> CreatePaymentAsync(Payment payment)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "payments";
            request.AddJsonBody(payment);
            return this.Sdk.ExecuteAsync<ItemResult<Payment>>(request);
        }

        public Task<ItemResult<Payment>> UpdatePaymentAsync(Guid payment_id, Payment payment)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "payments/{payment_id}";
            request.AddUrlSegment("payment_id", payment_id.ToString());
            request.AddJsonBody(payment);
            return this.Sdk.ExecuteAsync<ItemResult<Payment>>(request);
        }

        

        public Task<ActionResult> DeletePaymentAsync(Guid payment_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "payments/{payment_id}";
            request.AddUrlSegment("payment_id", payment_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
