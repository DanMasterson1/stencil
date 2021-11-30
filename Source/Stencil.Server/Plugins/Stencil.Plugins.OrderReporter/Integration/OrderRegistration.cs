using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Primary.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stencil.Plugins.OrderInformant.Integration
{
    public class OrderRegistration : ChokeableClass
    {
        // add this plugin to the product subscription
        public OrderRegistration(IFoundation iFoundation)
            : base(iFoundation)
        {
            this.OrderSubscription = iFoundation.Resolve<IWorkerSubscription>();
            this.WorkerRecipient = iFoundation.Resolve<IWorkerSubscriber>();
        }

        public IWorkerSubscription OrderSubscription { get; private set; }
        public IWorkerSubscriber WorkerRecipient { get; private set; }
        public void RegisterSelf()
        {
            base.ExecuteMethod(nameof(RegisterSelf), delegate ()
            {
                this.OrderSubscription.AddSubscriber(WorkerRecipient);
            });

        }
    }
}