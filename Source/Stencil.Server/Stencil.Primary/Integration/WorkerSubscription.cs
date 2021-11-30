using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Primary.Workers;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Common;

namespace Stencil.Primary.Integration
{
    public class WorkerSubscription : ChokeableClass, IWorkerSubscription
    {
        public WorkerSubscription(IFoundation iFoundation)
            : base(iFoundation)
        {
            this.NotifyPluginWorker = iFoundation.Resolve<INotifyPlugin>();

        }

        public INotifyPlugin NotifyPluginWorker { get; private set; }

        public void AddSubscriber(IWorkerSubscriber subscriber)
        {

            this.NotifyPluginWorker.WorkerSubscribers.Add(subscriber);

        }
    }
}
