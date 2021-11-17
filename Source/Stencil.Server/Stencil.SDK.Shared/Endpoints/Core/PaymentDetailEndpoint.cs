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
    public partial class PaymentDetailEndpoint : EndpointBase
    {
        public PaymentDetailEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<PaymentDetail>> GetPaymentDetailAsync(Guid paymentdetail_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "paymentdetails/{paymentdetail_id}";
            request.AddUrlSegment("paymentdetail_id", paymentdetail_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<PaymentDetail>>(request);
        }
        
        
        public Task<ListResult<PaymentDetail>> GetPaymentDetailByAccountIdAsync(Guid account_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "paymentdetails/by_accountid/{account_id}";
            request.AddUrlSegment("account_id", account_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<PaymentDetail>>(request);
        }
        

        public Task<ItemResult<PaymentDetail>> CreatePaymentDetailAsync(PaymentDetail paymentdetail)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "paymentdetails";
            request.AddJsonBody(paymentdetail);
            return this.Sdk.ExecuteAsync<ItemResult<PaymentDetail>>(request);
        }

        public Task<ItemResult<PaymentDetail>> UpdatePaymentDetailAsync(Guid paymentdetail_id, PaymentDetail paymentdetail)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "paymentdetails/{paymentdetail_id}";
            request.AddUrlSegment("paymentdetail_id", paymentdetail_id.ToString());
            request.AddJsonBody(paymentdetail);
            return this.Sdk.ExecuteAsync<ItemResult<PaymentDetail>>(request);
        }

        

        public Task<ActionResult> DeletePaymentDetailAsync(Guid paymentdetail_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "paymentdetails/{paymentdetail_id}";
            request.AddUrlSegment("paymentdetail_id", paymentdetail_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
