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
    partial class OrderSynchronizer
    {
        partial void HydrateSDKModel(Order domainModel, sdk.Order sdkModel)
        {
            
            List<LineItem> lineItems = this.API.Direct.LineItems.GetByOrderId(domainModel.order_id).Where(x => x.deleted_utc == null).ToList();
            
            if(sdkModel.payment_id != null)
            {
                Payment payment = this.API.Direct.Payments.GetById((Guid)sdkModel.payment_id);
                sdkModel.payment_cardtype = payment.card_type.ToString();
            }

            if (sdkModel.shipment_id != null)
            {
                Shipment shipment = this.API.Direct.Shipments.GetById((Guid)sdkModel.shipment_id);
                sdkModel.shipment_address = $"{shipment.shipment_street} {shipment.shipment_city}, {shipment.shipment_state} {shipment.shipment_zip}";
            }
           
            Account account = this.API.Direct.Accounts.GetById(sdkModel.account_id);
            sdkModel.account_name = $"{account.first_name} {account.last_name}";
            sdkModel.account_email = $"{account.email}";
            
            sdkModel.status = domainModel.order_status.ToString();
            sdkModel.created_utc = domainModel.created_utc;
            

            foreach(LineItem item in lineItems)
            {
                Listing referenceListing = this.API.Direct.Listings.GetById(item.listing_id);

                Product referenceProduct = this.API.Direct.Products.GetById(referenceListing.product_id);

                if(referenceListing.promotion_id != null)
                {
                    Promotion referencePromotion = this.API.Direct.Promotions.GetById((Guid)referenceListing.promotion_id);
                    decimal listingPrice = referenceProduct.baseprice - (referencePromotion.percent * referenceProduct.baseprice);

                    sdkModel.order_total += listingPrice * item.lineitem_quantity;
                }
                else
                {
                    decimal listingPrice = referenceProduct.baseprice;
                    sdkModel.order_total += listingPrice * item.lineitem_quantity;
                }

            }

        }
    }
}
