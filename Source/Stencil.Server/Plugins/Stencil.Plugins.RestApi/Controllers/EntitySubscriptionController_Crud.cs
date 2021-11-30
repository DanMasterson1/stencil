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
    [RoutePrefix("api/entitysubscriptions")]
    public partial class EntitySubscriptionController : HealthRestApiController
    {
        public EntitySubscriptionController(IFoundation foundation)
            : base(foundation, "EntitySubscription")
        {
        }

        [HttpGet]
        [Route("{subscription_id}")]
        public object GetById(Guid subscription_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.EntitySubscription result = this.API.Index.EntitySubscriptions.GetById(subscription_id);
                if (result == null)
                {
                    return Http404("EntitySubscription");
                }

                

                return base.Http200(new ItemResult<sdk.EntitySubscription>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("by_brandid/{brand_id}")]
        public object GetByBrandId(Guid brand_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByBrandId", delegate ()
            {
                
                
                ListResult<sdk.EntitySubscription> result = this.API.Index.EntitySubscriptions.GetByBrandId(brand_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.EntitySubscription entitysubscription)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(entitysubscription, "EntitySubscription");

                dm.EntitySubscription insert = entitysubscription.ToDomainModel();

                
                insert = this.API.Direct.EntitySubscriptions.Insert(insert);
                

                
                sdk.EntitySubscription result = insert.ToSDKModel();

                return base.Http201(new ItemResult<sdk.EntitySubscription>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/entitysubscription/{0}", entitysubscription.subscription_id));

            });

        }


        [HttpPut]
        [Route("{subscription_id}")]
        public object Update(Guid subscription_id, sdk.EntitySubscription entitysubscription)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(entitysubscription, "EntitySubscription");
                this.ValidateRouteMatch(subscription_id, entitysubscription.subscription_id, "EntitySubscription");

                entitysubscription.subscription_id = subscription_id;
                dm.EntitySubscription update = entitysubscription.ToDomainModel();


                update = this.API.Direct.EntitySubscriptions.Update(update);
                
                
                sdk.EntitySubscription existing = this.API.Direct.EntitySubscriptions.GetById(update.subscription_id).ToSDKModel();
                
                return base.Http200(new ItemResult<sdk.EntitySubscription>()
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
                dm.EntitySubscription delete = this.API.Direct.EntitySubscriptions.GetById(subscription_id);
                
                
                this.API.Direct.EntitySubscriptions.Delete(subscription_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = subscription_id.ToString()
                });
            });
        }

    }
}

