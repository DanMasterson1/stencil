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
    public partial class InvoiceController 
    {
        [HttpGet]
        [Route("regen_invoice/{order_id}")]
        public object RegenerateInvoice(Guid order_id)
        {
            return base.ExecuteFunction(nameof(RegenerateInvoice), delegate ()
            {
                //Guid? invoice_id = this.API.Direct.Orders.RegenerateInvoice(order_id);
                // maybe some kind of processing ID?
                this.API.Direct.Invoices.RegenerateInvoice(order_id);

                ItemResult<Guid> result = new ItemResult<Guid>()
                {
                    // item = invoice_id,
                    success = true
                };
                return base.Http200(result);
            });
        }

    }
}