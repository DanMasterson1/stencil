using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Primary.Integration;
using Stencil.Primary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stencil.Plugins.ProductInformant.Integration
{
    public class WorkRecipient : ChokeableClass, IWorkerSubscriber
    {
        public WorkRecipient(IFoundation iFoundation)
         : base(iFoundation)
        {
            this.ProductInformer = iFoundation.Resolve<IInform>();
            this.SubscribingEvent = "ProductQueried";
         
        }
       
        public IInform ProductInformer { get; private set; }
        public string SubscribingEvent { get; set ; }

        public void NotifyPlugin(NotifyPluginRequest request)
        {
            ProductInformer.Inform(request);
        }
    }
}