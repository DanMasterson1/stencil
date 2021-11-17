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
    public partial class PaymentTransactionEndpoint : EndpointBase
    {
        public PaymentTransactionEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<PaymentTransaction>> GetPaymentTransactionAsync(Guid paymenttransaction_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "paymenttransactions/{paymenttransaction_id}";
            request.AddUrlSegment("paymenttransaction_id", paymenttransaction_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<PaymentTransaction>>(request);
        }
        
        
        public Task<ListResult<PaymentTransaction>> GetPaymentTransactionByOrderIdAsync(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "paymenttransactions/by_orderid/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<PaymentTransaction>>(request);
        }
        
        public Task<ListResult<PaymentTransaction>> GetPaymentTransactionByPaymentIdAsync(Guid payment_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "paymenttransactions/by_paymentid/{payment_id}";
            request.AddUrlSegment("payment_id", payment_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<PaymentTransaction>>(request);
        }
        

        public Task<ItemResult<PaymentTransaction>> CreatePaymentTransactionAsync(PaymentTransaction paymenttransaction)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "paymenttransactions";
            request.AddJsonBody(paymenttransaction);
            return this.Sdk.ExecuteAsync<ItemResult<PaymentTransaction>>(request);
        }

        public Task<ItemResult<PaymentTransaction>> UpdatePaymentTransactionAsync(Guid paymenttransaction_id, PaymentTransaction paymenttransaction)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "paymenttransactions/{paymenttransaction_id}";
            request.AddUrlSegment("paymenttransaction_id", paymenttransaction_id.ToString());
            request.AddJsonBody(paymenttransaction);
            return this.Sdk.ExecuteAsync<ItemResult<PaymentTransaction>>(request);
        }

        

        public Task<ActionResult> DeletePaymentTransactionAsync(Guid paymenttransaction_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "paymenttransactions/{paymenttransaction_id}";
            request.AddUrlSegment("paymenttransaction_id", paymenttransaction_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
