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
    public partial class ListingEndpoint : EndpointBase
    {
        public ListingEndpoint(StencilSDK api)
            : base(api)
        {

        }
        
        public Task<ItemResult<Listing>> GetListingAsync(Guid listing_id)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "listings/{listing_id}";
            request.AddUrlSegment("listing_id", listing_id.ToString());
            
            return this.Sdk.ExecuteAsync<ItemResult<Listing>>(request);
        }
        
        public Task<ListResult<Listing>> Find(int skip = 0, int take = 10, string keyword = "", string order_by = "", bool descending = false, Guid? brand_id = null, Guid? product_id = null, Guid? promotion_id = null)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "listings";
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            request.AddParameter("keyword", keyword);
            request.AddParameter("brand_id", brand_id);
            request.AddParameter("product_id", product_id);
            request.AddParameter("promotion_id", promotion_id);
            
            
            return this.Sdk.ExecuteAsync<ListResult<Listing>>(request);
        }
        public Task<ListResult<Listing>> GetListingByBrandIdAsync(Guid brand_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "listings/by_brandid/{brand_id}";
            request.AddUrlSegment("brand_id", brand_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Listing>>(request);
        }
        
        public Task<ListResult<Listing>> GetListingByProductIdAsync(Guid product_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "listings/by_productid/{product_id}";
            request.AddUrlSegment("product_id", product_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Listing>>(request);
        }
        
        public Task<ListResult<Listing>> GetListingByPromotionIdAsync(Guid promotion_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "listings/by_promotionid/{promotion_id}";
            request.AddUrlSegment("promotion_id", promotion_id.ToString());
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("order_by", order_by);
            request.AddParameter("descending", descending);
            
            return this.Sdk.ExecuteAsync<ListResult<Listing>>(request);
        }
        

        public Task<ItemResult<Listing>> CreateListingAsync(Listing listing)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "listings";
            request.AddJsonBody(listing);
            return this.Sdk.ExecuteAsync<ItemResult<Listing>>(request);
        }

        public Task<ItemResult<Listing>> UpdateListingAsync(Guid listing_id, Listing listing)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "listings/{listing_id}";
            request.AddUrlSegment("listing_id", listing_id.ToString());
            request.AddJsonBody(listing);
            return this.Sdk.ExecuteAsync<ItemResult<Listing>>(request);
        }

        

        public Task<ActionResult> DeleteListingAsync(Guid listing_id)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "listings/{listing_id}";
            request.AddUrlSegment("listing_id", listing_id.ToString());
            return this.Sdk.ExecuteAsync<ActionResult>(request);
        }
    }
}
