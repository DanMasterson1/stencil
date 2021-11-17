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
    partial class ShipmentSynchronizer
    {
        partial void HydrateSDKModel(Shipment domainModel, sdk.Shipment sdkModel)
        {
            Order domainOrder = this.API.Direct.Orders.GetById(domainModel.order_id);

            if (domainOrder != null)
            {
                domainOrder.shipment_id = sdkModel.shipment_id;
                
                if (domainOrder.order_status == OrderStatus.Open)
                {
                    domainOrder.order_status = OrderStatus.Processing;
                }

                this.API.Direct.Orders.Update(domainOrder);
            }
        }
    }
}
