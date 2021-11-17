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
    public partial class ShipmentEndpoint : EndpointBase
    {
        public ShipmentEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Shipment>> GetShipmentAsync(Guid shipment_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "shipments/{shipment_id}";
            request.AddUrlSegment("shipment_id", shipment_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Shipment>>(request);
        }
        
        
        public Task<ListResult<Shipment>> GetShipmentByOrderIdAsync(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "shipments/by_orderid/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Shipment>>(request);
        }
        

        public Task<ItemResult<Shipment>> CreateShipmentAsync(Shipment shipment)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "shipments";
            request.AddJsonBody(shipment);
            return this.Sdk.ExecuteAsync<ItemResult<Shipment>>(request);
        }

        public Task<ItemResult<Shipment>> UpdateShipmentAsync(Guid shipment_id, Shipment shipment)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "shipments/{shipment_id}";
            request.AddUrlSegment("shipment_id", shipment_id.ToString());
            request.AddJsonBody(shipment);
            return this.Sdk.ExecuteAsync<ItemResult<Shipment>>(request);
        }

        

        public Task<ActionResult> DeleteShipmentAsync(Guid shipment_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "shipments/{shipment_id}";
            request.AddUrlSegment("shipment_id", shipment_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
