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
    public partial class ListingSynchronizer 
    {
        partial void HydrateSDKModel(Listing domainModel, sdk.Listing sdkModel)
        {

            // baseprice set in sdk model
            sdk.Product referenceProduct = this.API.Index.Products.GetById(sdkModel.product_id);
            if(referenceProduct != null)
            {
                sdkModel.product_baseprice = referenceProduct.baseprice;
            }
            else
            {
                Product referenceDomainProduct = this.API.Direct.Products.GetById(sdkModel.product_id);
                if (referenceDomainProduct != null)
                {
                    sdkModel.product_baseprice = referenceDomainProduct.baseprice;
                }
            }

            // promotion percent set in sdk model
            if(sdkModel.promotion_id != null)
            {
                sdk.Promotion referencePromotion = this.API.Index.Promotions.GetById((Guid)sdkModel.promotion_id);
                if (referencePromotion != null)
                {
                    sdkModel.promotion_percent = referencePromotion.percent;
                    sdkModel.promotion_description = referencePromotion.promotion_description;
                }
                else
                {
                    Promotion referenceDomainPromotion = this.API.Direct.Promotions.GetById((Guid)sdkModel.promotion_id);
                    if (referenceDomainPromotion != null)
                    {
                        sdkModel.promotion_percent = referenceDomainPromotion.percent;
                        sdkModel.promotion_description = referenceDomainPromotion.promotion_description;
                    }
                }
            }

            // listing price set by above 2
            if (sdkModel.promotion_percent != null)
            {
                sdkModel.listing_price = sdkModel.product_baseprice - ((decimal)sdkModel.promotion_percent * sdkModel.product_baseprice);
            }
            else
            {
                sdkModel.listing_price = sdkModel.product_baseprice;
            }


        }
    }
}
