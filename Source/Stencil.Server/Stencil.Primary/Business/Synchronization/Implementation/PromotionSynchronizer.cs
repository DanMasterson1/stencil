using System;
using Codeable.Foundation.Common;
using Stencil.Common;
using Stencil.Domain;
using Stencil.Primary.Business.Index;
using Stencil.Primary.Health;
using sdk = Stencil.SDK.Models;
using System.Collections.Generic;
using System.Linq;
using Codeable.Foundation.Core;

namespace Stencil.Primary.Synchronization.Implementation
{
    partial class PromotionSynchronizer
    {
        partial void HydrateSDKModel(Promotion domainModel, sdk.Promotion sdkModel)
        {
            sdkModel.promotion_name = domainModel.promotion_type.ToString();
        }
    }
}
