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
    [RoutePrefix("api/orders")]
    public partial class OrderController : HealthRestApiController
    {
        public OrderController(IFoundation foundation)
            : base(foundation, "Order")
        {
        }

        [HttpGet]
        [Route("{order_id}")]
        public object GetById(Guid order_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Order result = this.API.Index.Orders.GetById(order_id);
                if (result == null)
                {
                    return Http404("Order");
                }

                

                return base.Http200(new ItemResult<sdk.Order>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("by_accountid/{account_id}")]
        public object GetByAccountId(Guid account_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByAccountId", delegate ()
            {
                
                
                ListResult<sdk.Order> result = this.API.Index.Orders.GetByAccountId(account_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        [HttpGet]
        [Route("by_invoiceid/{invoice_id}")]
        public object GetByInvoiceId(Guid invoice_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByInvoiceId", delegate ()
            {
                
                
                ListResult<sdk.Order> result = this.API.Index.Orders.GetByInvoiceId(invoice_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        [HttpGet]
        [Route("by_paymentid/{payment_id}")]
        public object GetByPaymentId(Guid payment_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByPaymentId", delegate ()
            {
                
                
                ListResult<sdk.Order> result = this.API.Index.Orders.GetByPaymentId(payment_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        [HttpGet]
        [Route("by_shipmentid/{shipment_id}")]
        public object GetByShipmentId(Guid shipment_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByShipmentId", delegate ()
            {
                
                
                ListResult<sdk.Order> result = this.API.Index.Orders.GetByShipmentId(shipment_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Order order)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(order, "Order");

                dm.Order insert = order.ToDomainModel();

                
                insert = this.API.Direct.Orders.Insert(insert);
                

                
                sdk.Order result = this.API.Index.Orders.GetById(insert.order_id);

                return base.Http201(new ItemResult<sdk.Order>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/order/{0}", order.order_id));

            });

        }


        [HttpPut]
        [Route("{order_id}")]
        public object Update(Guid order_id, sdk.Order order)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(order, "Order");
                this.ValidateRouteMatch(order_id, order.order_id, "Order");

                order.order_id = order_id;
                dm.Order update = order.ToDomainModel();


                update = this.API.Direct.Orders.Update(update);
                
                
                sdk.Order existing = this.API.Index.Orders.GetById(update.order_id);
                
                
                return base.Http200(new ItemResult<sdk.Order>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{order_id}")]
        public object Delete(Guid order_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Order delete = this.API.Direct.Orders.GetById(order_id);
                
                
                this.API.Direct.Orders.Delete(order_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = order_id.ToString()
                });
            });
        }

    }
}

