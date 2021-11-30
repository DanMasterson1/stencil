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
    [RoutePrefix("api/subscriptions")]
    public partial class SubscriptionController : HealthRestApiController
    {
        public SubscriptionController(IFoundation foundation)
            : base(foundation, "Subscription")
        {
        }

        //[HttpGet]
        //[Route("{subscription_id}")]
        //public object GetById(Guid subscription_id)
        //{
        //    return base.ExecuteFunction<object>("GetById", delegate()
        //    {
        //        sdk.Subscription result = this.API.Index.Subscriptions.GetById(subscription_id);
        //        if (result == null)
        //        {
        //            return Http404("Subscription");
        //        }

                

        //        return base.Http200(new ItemResult<sdk.Subscription>()
        //        {
        //            success = true, 
        //            item = result
        //        });
        //    });
        //}
        
        
        //[HttpGet]
        //[Route("by_brandid/{brand_id}")]
        //public object GetByBrandId(Guid brand_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        //{
        //    return base.ExecuteFunction<object>("GetByBrandId", delegate ()
        //    {
                
                
        //        ListResult<sdk.Subscription> result = this.API.Index.Subscriptions.GetByBrandId(brand_id, skip, take, order_by, descending);
        //        result.success = true;
        //        return base.Http200(result);
        //    });
        //}
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Subscription subscription)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(subscription, "Subscription");

                dm.Subscription insert = subscription.ToDomainModel();

                
                insert = this.API.Direct.Subscriptions.Insert(insert);
                

                
                sdk.Subscription result = insert.ToSDKModel();

                return base.Http201(new ItemResult<sdk.Subscription>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/subscription/{0}", subscription.subscription_id));

            });

        }


        [HttpPut]
        [Route("{subscription_id}")]
        public object Update(Guid subscription_id, sdk.Subscription subscription)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(subscription, "Subscription");
                this.ValidateRouteMatch(subscription_id, subscription.subscription_id, "Subscription");

                subscription.subscription_id = subscription_id;
                dm.Subscription update = subscription.ToDomainModel();


                update = this.API.Direct.Subscriptions.Update(update);
                
                
                sdk.Subscription existing = this.API.Direct.Subscriptions.GetById(update.subscription_id).ToSDKModel();
                
                return base.Http200(new ItemResult<sdk.Subscription>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{subscription_id}")]
        public object Delete(Guid subscription_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Subscription delete = this.API.Direct.Subscriptions.GetById(subscription_id);
                
                
                this.API.Direct.Subscriptions.Delete(subscription_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = subscription_id.ToString()
                });
            });
        }

    }
}

