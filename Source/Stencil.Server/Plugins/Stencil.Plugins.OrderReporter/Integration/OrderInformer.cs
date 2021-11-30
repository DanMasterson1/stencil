using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Newtonsoft.Json;
using Stencil.Domain;
using Stencil.Plugins.OrderInformant.Models;
using Stencil.Primary;
using Stencil.Primary.Models;
using System.Collections.Generic;

namespace Stencil.Plugins.OrderInformant.Integration
{
    public class OrderInformer : ChokeableClass, IInform
    {
        public OrderInformer(IFoundation foundation)
          : base(foundation)
        {
            this.API = foundation.Resolve<StencilAPI>();
            this.SubscribingEvent = "ProductOnOrder";
            this.ProcessNotification = foundation.Resolve<ProcessNotify>();
        }

        public StencilAPI API { get; private set; }
        public string SubscribingEvent { get; set; }

        public ProcessNotify ProcessNotification { get; set; }

        public void Inform(NotifyPluginRequest request)
        {
            base.ExecuteMethod(nameof(Inform), delegate ()
            {
                List<Subscription> eventSubscriptions = this.API.Direct.Subscriptions.GetByBrandAndEvent(request.brand_id, request.eventName);

                OrderNotification notification = new OrderNotification()
                {
                    payload = JsonConvert.SerializeObject(request),
                    subscriptions = eventSubscriptions
                };

                ProcessNotification.Send(notification);
            });
        }
    }
}