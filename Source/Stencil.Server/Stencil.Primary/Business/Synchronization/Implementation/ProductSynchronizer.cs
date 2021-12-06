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
    partial class ProductSynchronizer
    {
        partial void HydrateSDKModel(Product domainModel, sdk.Product sdkModel)
        {

            List<Listing> listings = this.API.Direct.Listings.GetByProductId(sdkModel.product_id);

            if(listings.Any(x => x.promotion_id != null))
            {
                sdkModel.promotional = true;
            }

        }
    }
}
