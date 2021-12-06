using Codeable.Foundation.Common;
using Stencil.Primary.Daemons;
using System;
using Stencil.Primary.Models;
using Stencil.Primary.Integration;
using System.Collections.Generic;
using System.Linq;

namespace Stencil.Primary.Workers
{
    public class NotifyPluginWorker : WorkerBase<NotifyPluginRequest>, INotifyPlugin
    {
        public const string WORKER_NAME = nameof(NotifyPluginWorker);
        public StencilAPI API { get; set; }
        public List<IWorkerSubscriber> WorkerSubscribers { get; private set; }

        public NotifyPluginWorker(IFoundation iFoundation)
            : base(iFoundation, WORKER_NAME)
        {
            this.API = iFoundation.Resolve<StencilAPI>();
            this.WorkerSubscribers = new List<IWorkerSubscriber>();
        }

        public static void EnqueueRequest(IFoundation foundation, NotifyPluginRequest request)
        {
            EnqueueRequest<NotifyPluginWorker>(foundation, WORKER_NAME, request, (int)TimeSpan.FromMinutes(2).TotalMilliseconds); 
        }

        protected override void ProcessRequest(NotifyPluginRequest request)
        {
            base.ExecuteMethod(nameof(ProcessRequest), delegate ()
            {
                NotifyPlugin(request);

            });
        }

        private  void NotifyPlugin(NotifyPluginRequest request)
        {
            base.ExecuteMethod(nameof(NotifyPlugin), delegate ()
            {
                List<IWorkerSubscriber> relevantSubscribers = this.WorkerSubscribers.Where(x => x.SubscribingEvent == request.eventName).ToList();

                foreach (IWorkerSubscriber workerSubscriber in relevantSubscribers)
                {
                    workerSubscriber.NotifyPlugin(request);    
                }
            });

        }

    }
}
