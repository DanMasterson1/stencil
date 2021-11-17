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
    public partial class LineItemEndpoint : EndpointBase
    {
        public LineItemEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<LineItem>> GetLineItemAsync(Guid lineitem_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "lineitems/{lineitem_id}";
            request.AddUrlSegment("lineitem_id", lineitem_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<LineItem>>(request);
        }
        
        
        public Task<ListResult<LineItem>> GetLineItemByOrderIdAsync(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "lineitems/by_orderid/{order_id}";
            request.AddUrlSegment("order_id", order_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<LineItem>>(request);
        }
        
        public Task<ListResult<LineItem>> GetLineItemByListingIdAsync(Guid listing_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "lineitems/by_listingid/{listing_id}";
            request.AddUrlSegment("listing_id", listing_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<LineItem>>(request);
        }
        

        public Task<ItemResult<LineItem>> CreateLineItemAsync(LineItem lineitem)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "lineitems";
            request.AddJsonBody(lineitem);
            return this.Sdk.ExecuteAsync<ItemResult<LineItem>>(request);
        }

        public Task<ItemResult<LineItem>> UpdateLineItemAsync(Guid lineitem_id, LineItem lineitem)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "lineitems/{lineitem_id}";
            request.AddUrlSegment("lineitem_id", lineitem_id.ToString());
            request.AddJsonBody(lineitem);
            return this.Sdk.ExecuteAsync<ItemResult<LineItem>>(request);
        }

        

        public Task<ActionResult> DeleteLineItemAsync(Guid lineitem_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "lineitems/{lineitem_id}";
            request.AddUrlSegment("lineitem_id", lineitem_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
