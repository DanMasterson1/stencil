using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Domain;
using Stencil.Data.Sql;
using Stencil.Primary.Synchronization;

namespace Stencil.Primary.Business.Direct.Implementation
{
    partial class ShipmentBusiness
    {
        partial void AfterInsertPersisted(StencilContext db, dbShipment shipment)
        {
            Order domainOrder = this.API.Direct.Orders.GetById(shipment.order_id);

            if (domainOrder != null)
            {
                domainOrder.order_shipped = true;
                this.API.Direct.Orders.Update(domainOrder);
            }
        }
    }
}
