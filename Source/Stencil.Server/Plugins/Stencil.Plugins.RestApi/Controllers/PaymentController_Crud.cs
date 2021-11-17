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
    [RoutePrefix("api/payments")]
    public partial class PaymentController : HealthRestApiController
    {
        public PaymentController(IFoundation foundation)
            : base(foundation, "Payment")
        {
        }

        [HttpGet]
        [Route("{payment_id}")]
        public object GetById(Guid payment_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Payment result = this.API.Index.Payments.GetById(payment_id);
                if (result == null)
                {
                    return Http404("Payment");
                }

                

                return base.Http200(new ItemResult<sdk.Payment>()
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
                
                
                ListResult<sdk.Payment> result = this.API.Index.Payments.GetByOrderId(order_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Payment payment)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(payment, "Payment");

                dm.Payment insert = payment.ToDomainModel();

                
                insert = this.API.Direct.Payments.Insert(insert);
                

                
                sdk.Payment result = this.API.Index.Payments.GetById(insert.payment_id);

                return base.Http201(new ItemResult<sdk.Payment>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/payment/{0}", payment.payment_id));

            });

        }


        [HttpPut]
        [Route("{payment_id}")]
        public object Update(Guid payment_id, sdk.Payment payment)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(payment, "Payment");
                this.ValidateRouteMatch(payment_id, payment.payment_id, "Payment");

                payment.payment_id = payment_id;
                dm.Payment update = payment.ToDomainModel();


                update = this.API.Direct.Payments.Update(update);
                
                
                sdk.Payment existing = this.API.Index.Payments.GetById(update.payment_id);
                
                
                return base.Http200(new ItemResult<sdk.Payment>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{payment_id}")]
        public object Delete(Guid payment_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Payment delete = this.API.Direct.Payments.GetById(payment_id);
                
                
                this.API.Direct.Payments.Delete(payment_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = payment_id.ToString()
                });
            });
        }

    }
}

