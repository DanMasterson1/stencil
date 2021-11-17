using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Plugins.ProductReporter.Controllers;
using Stencil.Primary.Integration;

namespace Stencil.Plugins.ProductReporter.Integration
{
    public class ProductRegistration : ChokeableClass
    {
        // add this plugin to the product subscription
        public  ProductRegistration(IFoundation iFoundation)
            : base(iFoundation)
        {
            this.ProductSubscription = iFoundation.Resolve<IProductSubscription>();
            this.ProductNotify = iFoundation.Resolve<IProductNotify>();
        }

        public IProductSubscription ProductSubscription { get; private set; }
        public IProductNotify ProductNotify { get; private set; }
        public void RegisterSelf()
        {
            base.ExecuteMethod(nameof(RegisterSelf), delegate ()
            {
                this.ProductSubscription.AddSubscriber(ProductNotify);
            });
  
        }
    }
}