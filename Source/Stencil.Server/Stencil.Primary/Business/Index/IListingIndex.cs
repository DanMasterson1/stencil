using Codeable.Foundation.Common;
using Stencil.SDK.Models;
using Stencil.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Index
{
    partial interface IListingIndex
    {
        ListResult<Listing> GetPromotionalListingsForBrandInPriceRange(Guid promotion_id,int floor, int ceiling, string keyword="");

        ListResult<Listing> GetCloseOutDeals(Guid brand_id, Guid promotion_id, int max_price);
        
        ListResult<Listing> GetAssociatedListings(Guid brand_id);
    }
}
