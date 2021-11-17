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
    [RoutePrefix("api/paymentdetails")]
    public partial class PaymentDetailController : HealthRestApiController
    {
        public PaymentDetailController(IFoundation foundation)
            : base(foundation, "PaymentDetail")
        {
        }

        [HttpGet]
        [Route("{paymentdetail_id}")]
        public object GetById(Guid paymentdetail_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.PaymentDetail result = this.API.Index.PaymentDetails.GetById(paymentdetail_id);
                if (result == null)
                {
                    return Http404("PaymentDetail");
                }

                

                return base.Http200(new ItemResult<sdk.PaymentDetail>()
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
                
                
                ListResult<sdk.PaymentDetail> result = this.API.Index.PaymentDetails.GetByAccountId(account_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.PaymentDetail paymentdetail)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(paymentdetail, "PaymentDetail");

                dm.PaymentDetail insert = paymentdetail.ToDomainModel();

                
                insert = this.API.Direct.PaymentDetails.Insert(insert);
                

                
                sdk.PaymentDetail result = this.API.Index.PaymentDetails.GetById(insert.paymentdetail_id);

                return base.Http201(new ItemResult<sdk.PaymentDetail>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/paymentdetail/{0}", paymentdetail.paymentdetail_id));

            });

        }


        [HttpPut]
        [Route("{paymentdetail_id}")]
        public object Update(Guid paymentdetail_id, sdk.PaymentDetail paymentdetail)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(paymentdetail, "PaymentDetail");
                this.ValidateRouteMatch(paymentdetail_id, paymentdetail.paymentdetail_id, "PaymentDetail");

                paymentdetail.paymentdetail_id = paymentdetail_id;
                dm.PaymentDetail update = paymentdetail.ToDomainModel();


                update = this.API.Direct.PaymentDetails.Update(update);
                
                
                sdk.PaymentDetail existing = this.API.Index.PaymentDetails.GetById(update.paymentdetail_id);
                
                
                return base.Http200(new ItemResult<sdk.PaymentDetail>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{paymentdetail_id}")]
        public object Delete(Guid paymentdetail_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.PaymentDetail delete = this.API.Direct.PaymentDetails.GetById(paymentdetail_id);
                
                
                this.API.Direct.PaymentDetails.Delete(paymentdetail_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = paymentdetail_id.ToString()
                });
            });
        }

    }
}

