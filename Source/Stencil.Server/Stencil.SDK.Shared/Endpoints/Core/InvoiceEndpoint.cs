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
    public partial class InvoiceEndpoint : EndpointBase
    {
        public InvoiceEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Invoice>> GetInvoiceAsync(Guid invoice_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "invoices/{invoice_id}";
            request.AddUrlSegment("invoice_id", invoice_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Invoice>>(request);
        }
        
        
        public Task<ListResult<Invoice>> GetInvoiceByOrderIdAsync(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "invoices/by_orderid/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Invoice>>(request);
        }
        
        public Task<ListResult<Invoice>> GetInvoiceByAssetIdAsync(Guid asset_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "invoices/by_assetid/{asset_id}";
            request.AddUrlSegment("asset_id", asset_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Invoice>>(request);
        }
        

        public Task<ItemResult<Invoice>> CreateInvoiceAsync(Invoice invoice)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "invoices";
            request.AddJsonBody(invoice);
            return this.Sdk.ExecuteAsync<ItemResult<Invoice>>(request);
        }

        public Task<ItemResult<Invoice>> UpdateInvoiceAsync(Guid invoice_id, Invoice invoice)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "invoices/{invoice_id}";
            request.AddUrlSegment("invoice_id", invoice_id.ToString());
            request.AddJsonBody(invoice);
            return this.Sdk.ExecuteAsync<ItemResult<Invoice>>(request);
        }

        

        public Task<ActionResult> DeleteInvoiceAsync(Guid invoice_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "invoices/{invoice_id}";
            request.AddUrlSegment("invoice_id", invoice_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
