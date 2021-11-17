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
    [RoutePrefix("api/paymenttransactions")]
    public partial class PaymentTransactionController : HealthRestApiController
    {
        public PaymentTransactionController(IFoundation foundation)
            : base(foundation, "PaymentTransaction")
        {
        }

        [HttpGet]
        [Route("{paymenttransaction_id}")]
        public object GetById(Guid paymenttransaction_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.PaymentTransaction result = this.API.Index.PaymentTransactions.GetById(paymenttransaction_id);
                if (result == null)
                {
                    return Http404("PaymentTransaction");
                }

                

                return base.Http200(new ItemResult<sdk.PaymentTransaction>()
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
                
                
                ListResult<sdk.PaymentTransaction> result = this.API.Index.PaymentTransactions.GetByOrderId(order_id, skip, take, order_by, descending);
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
                
                
                ListResult<sdk.PaymentTransaction> result = this.API.Index.PaymentTransactions.GetByPaymentId(payment_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.PaymentTransaction paymenttransaction)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(paymenttransaction, "PaymentTransaction");

                dm.PaymentTransaction insert = paymenttransaction.ToDomainModel();

                
                insert = this.API.Direct.PaymentTransactions.Insert(insert);
                

                
                sdk.PaymentTransaction result = this.API.Index.PaymentTransactions.GetById(insert.paymenttransaction_id);

                return base.Http201(new ItemResult<sdk.PaymentTransaction>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/paymenttransaction/{0}", paymenttransaction.paymenttransaction_id));

            });

        }


        [HttpPut]
        [Route("{paymenttransaction_id}")]
        public object Update(Guid paymenttransaction_id, sdk.PaymentTransaction paymenttransaction)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(paymenttransaction, "PaymentTransaction");
                this.ValidateRouteMatch(paymenttransaction_id, paymenttransaction.paymenttransaction_id, "PaymentTransaction");

                paymenttransaction.paymenttransaction_id = paymenttransaction_id;
                dm.PaymentTransaction update = paymenttransaction.ToDomainModel();


                update = this.API.Direct.PaymentTransactions.Update(update);
                
                
                sdk.PaymentTransaction existing = this.API.Index.PaymentTransactions.GetById(update.paymenttransaction_id);
                
                
                return base.Http200(new ItemResult<sdk.PaymentTransaction>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{paymenttransaction_id}")]
        public object Delete(Guid paymenttransaction_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.PaymentTransaction delete = this.API.Direct.PaymentTransactions.GetById(paymenttransaction_id);
                
                
                this.API.Direct.PaymentTransactions.Delete(paymenttransaction_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = paymenttransaction_id.ToString()
                });
            });
        }

    }
}

