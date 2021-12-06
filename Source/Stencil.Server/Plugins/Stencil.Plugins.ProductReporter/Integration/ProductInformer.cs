using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Newtonsoft.Json;
using Stencil.Domain;
using Stencil.Plugins.ProductInformant.Models;
using Stencil.Primary;
using Stencil.Primary.Models;
using System.Collections.Generic;

namespace Stencil.Plugins.ProductInformant.Integration
{
    public class ProductInformer : ChokeableClass, IInform
    {
        public ProductInformer(IFoundation foundation)
          : base(foundation)
        {
            this.API = new StencilAPI(foundation);
            this.Dispatcher = foundation.Resolve<IDispatchNotifications>();
        }

        public StencilAPI API { get; private set; }
        public IDispatchNotifications Dispatcher { get; set; }

        public void Inform(NotifyPluginRequest request)
        {
            base.ExecuteMethod(nameof(Inform), delegate ()
            {
                List<Subscription> eventSubscriptions = this.API.Direct.Subscriptions.GetByBrandAndEvent(request.brand_id, request.eventName);
                
                ProductNotification notification = new ProductNotification()
                {
                    payload = JsonConvert.SerializeObject(request),
                    subscriptions = eventSubscriptions
                };

                Dispatcher.Dispatch(notification);
            });
        }
    }
}