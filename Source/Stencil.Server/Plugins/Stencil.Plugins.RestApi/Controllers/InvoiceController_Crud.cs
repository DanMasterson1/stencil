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
    [ApiKeyHttpAuthorize]
    [RoutePrefix("api/invoices")]
    public partial class InvoiceController : HealthRestApiController
    {
        public InvoiceController(IFoundation foundation)
            : base(foundation, "Invoice")
        {
        }

        [HttpGet]
        [Route("{invoice_id}")]
        public object GetById(Guid invoice_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Invoice result = this.API.Index.Invoices.GetById(invoice_id);
                if (result == null)
                {
                    return Http404("Invoice");
                }

                

                return base.Http200(new ItemResult<sdk.Invoice>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("by_orderid/{order_id}")]
        public object GetByOrderId(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByOrderId", delegate ()
            {
                
                
                ListResult<sdk.Invoice> result = this.API.Index.Invoices.GetByOrderId(order_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        [HttpGet]
        [Route("by_assetid/{asset_id}")]
        public object GetByAssetId(Guid asset_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByAssetId", delegate ()
            {
                
                
                ListResult<sdk.Invoice> result = this.API.Index.Invoices.GetByAssetId(asset_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Invoice invoice)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(invoice, "Invoice");

                dm.Invoice insert = invoice.ToDomainModel();

                
                insert = this.API.Direct.Invoices.Insert(insert);
                

                
                sdk.Invoice result = this.API.Index.Invoices.GetById(insert.invoice_id);

                return base.Http201(new ItemResult<sdk.Invoice>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/invoice/{0}", invoice.invoice_id));

            });

        }


        [HttpPut]
        [Route("{invoice_id}")]
        public object Update(Guid invoice_id, sdk.Invoice invoice)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(invoice, "Invoice");
                this.ValidateRouteMatch(invoice_id, invoice.invoice_id, "Invoice");

                invoice.invoice_id = invoice_id;
                dm.Invoice update = invoice.ToDomainModel();


                update = this.API.Direct.Invoices.Update(update);
                
                
                sdk.Invoice existing = this.API.Index.Invoices.GetById(update.invoice_id);
                
                
                return base.Http200(new ItemResult<sdk.Invoice>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{invoice_id}")]
        public object Delete(Guid invoice_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Invoice delete = this.API.Direct.Invoices.GetById(invoice_id);
                
                
                this.API.Direct.Invoices.Delete(invoice_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = invoice_id.ToString()
                });
            });
        }

    }
}

