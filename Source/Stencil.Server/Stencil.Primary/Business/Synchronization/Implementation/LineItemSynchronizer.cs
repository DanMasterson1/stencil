using Codeable.Foundation.Common;
using Stencil.Common;
using Stencil.Domain;
using Stencil.Primary.Business.Index;
using Stencil.Primary.Health;
using sdk = Stencil.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Codeable.Foundation.Core;

namespace Stencil.Primary.Synchronization.Implementation
{
    public partial class LineItemSynchronizer
    {
        partial void HydrateSDKModel(LineItem domainModel, sdk.LineItem sdkModel)
        {
            if(sdkModel.listing_id != null)
            {
                sdk.Listing referenceListing = this.API.Index.Listings.GetById(sdkModel.listing_id);
                if(referenceListing != null)
                {
                    sdkModel.listing_price = referenceListing.listing_price;
                    // if listing has bogo then if the qty is > 1 - the listing_price
                    if(referenceListing.promotion_id != null)
                    {
                        Promotion referencePromotion = this.API.Direct.Promotions.GetById((Guid)referenceListing.promotion_id);

                        if(referencePromotion.promotion_type == PromotionType.Bogo && sdkModel.lineitem_quantity > 2)
                        {
                            sdkModel.lineitem_total = referenceListing.listing_price * (sdkModel.lineitem_quantity - 1);
                        }
                        else
                        {
                            sdkModel.lineitem_total = referenceListing.listing_price * sdkModel.lineitem_quantity;
                        }

                    }
                    
                }

            }
        }
    }
}
