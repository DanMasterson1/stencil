using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Primary.Integration;
using Stencil.Primary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stencil.Plugins.OrderInformant.Integration
{
    public class WorkRecipient : ChokeableClass, IWorkerSubscriber
    {
        public WorkRecipient(IFoundation iFoundation)
         : base(iFoundation)
        {
            this.OrderInformer = iFoundation.Resolve<IInform>();
            this.SubscribingEvent = "ProductOrdered";
         
        }
       
        public IInform OrderInformer { get; private set; }
        public string SubscribingEvent { get; set ; }

        public void NotifyPlugin(NotifyPluginRequest request)
        {
            OrderInformer.Inform(request);
        }
    }
}