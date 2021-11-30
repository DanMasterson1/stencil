using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Primary.Integration;

namespace Stencil.Plugins.ProductInformant.Integration
{
    public class ProductRegistration : ChokeableClass
    {
        // add this plugin to the product subscription
        public  ProductRegistration(IFoundation iFoundation)
            : base(iFoundation)
        {
            this.ProductSubscription = iFoundation.Resolve<IWorkerSubscription>();
            this.Plugin = iFoundation.Resolve<IWorkerSubscriber>();
        }

        public IWorkerSubscription ProductSubscription { get; private set; }
        public IWorkerSubscriber Plugin { get; private set; }
        public void RegisterSelf()
        {
            base.ExecuteMethod(nameof(RegisterSelf), delegate ()
            {
                this.ProductSubscription.AddSubscriber(Plugin);
            });
  
        }
    }
}