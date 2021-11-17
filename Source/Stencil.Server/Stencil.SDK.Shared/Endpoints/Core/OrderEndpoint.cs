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
    public partial class OrderEndpoint : EndpointBase
    {
        public OrderEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Order>> GetOrderAsync(Guid order_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "orders/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Order>>(request);
        }
        
        
        public Task<ListResult<Order>> GetOrderByAccountIdAsync(Guid account_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "orders/by_accountid/{account_id}";
            request.AddUrlSegment("account_id", account_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Order>>(request);
        }
        
        public Task<ListResult<Order>> GetOrderByInvoiceIdAsync(Guid invoice_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "orders/by_invoiceid/{invoice_id}";
            request.AddUrlSegment("invoice_id", invoice_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Order>>(request);
        }
        
        public Task<ListResult<Order>> GetOrderByPaymentIdAsync(Guid payment_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "orders/by_paymentid/{payment_id}";
            request.AddUrlSegment("payment_id", payment_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Order>>(request);
        }
        
        public Task<ListResult<Order>> GetOrderByShipmentIdAsync(Guid shipment_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "orders/by_shipmentid/{shipment_id}";
            request.AddUrlSegment("shipment_id", shipment_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Order>>(request);
        }
        

        public Task<ItemResult<Order>> CreateOrderAsync(Order order)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "orders";
            request.AddJsonBody(order);
            return this.Sdk.ExecuteAsync<ItemResult<Order>>(request);
        }

        public Task<ItemResult<Order>> UpdateOrderAsync(Guid order_id, Order order)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "orders/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            request.AddJsonBody(order);
            return this.Sdk.ExecuteAsync<ItemResult<Order>>(request);
        }

        

        public Task<ActionResult> DeleteOrderAsync(Guid order_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "orders/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
