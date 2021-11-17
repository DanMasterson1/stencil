using Stencil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Direct.Implementation
{
    public partial class InvoiceBusiness
    {
        public void RegenerateInvoice(Guid order_id)
        {
            Order order = this.API.Direct.Orders.GetById(order_id);

            if (order.invoice_id != null)
            {
                Invoice invoice = this.API.Direct.Invoices.GetById((Guid)order.invoice_id);
                Asset asset = this.API.Direct.Assets.GetById(invoice.asset_id);

                this.API.Direct.Invoices.Delete(invoice.invoice_id);
                //this.API.Direct.Assets.Delete(asset.asset_id);

                order.invoice_id = null;
                order.order_status = OrderStatus.Processing;
                order = this.API.Direct.Orders.Update(order);

                this.API.Integration.Synchronization.AgitateDaemon("InvoicerDaemon"); // dont do this as a string

            }

            // get the new invoices Id and if it isnt null cast it

            //TODO: make this return the new invoice guid
            //  Guid newInvoice_id = this.API.Direct.Orders.GetById(order_id).invoice_id; // returns null think because of race condition i.e its not done invoicing yet

            //return newInvoice_id;

        }

    }
}
