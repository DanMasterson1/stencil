using Codeable.Foundation.Common;
using Codeable.Foundation.Core;
using System;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using sdk = Stencil.SDK.Models;
using dm = Stencil.Domain;
using Stencil.Primary;
using Stencil.SDK;
using Stencil.Web.Controllers;
using Stencil.Web.Security;

namespace Stencil.Plugins.RestAPI.Controllers
{
    public partial class ListingController
    {
       [HttpGet]
       [Route("by_promotionrangekeyword")]
       public object GetPromotionalListingsForBrandInPriceRange(Guid promotion_id, int floor, int ceiling, string keyword = "")
        {
            return base.ExecuteFunction<object>(nameof(GetPromotionalListingsForBrandInPriceRange), delegate ()
            {
                ListResult<sdk.Listing> result = base.API.Index.Listings.GetPromotionalListingsForBrandInPriceRange(promotion_id, floor, ceiling, keyword);
                result.success = true;
                return base.Http200(result);
            });
        }

        [HttpGet]
        [Route("getcloseoutdeals")]
        public object GetCloseoutDeals(Guid brand_id, Guid promotion_id, int max_price)
        {
            return base.ExecuteFunction<object>(nameof(GetCloseoutDeals), delegate ()
            {
                ListResult<sdk.Listing> result = base.API.Index.Listings.GetCloseOutDeals(brand_id,promotion_id,max_price);
                result.success = true;
                return base.Http200(result);
            });
        }
    }
}