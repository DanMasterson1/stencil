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
    public class ProductSubscription : ChokeableClass, IProductSubscription
    {
        public ProductSubscription(IFoundation iFoundation)
            : base(iFoundation)
        {
            this.QueryReportWorker = iFoundation.Resolve<IQueryReportWorker>();

        }

        public IQueryReportWorker QueryReportWorker { get; private set; }

        public void AddSubscriber(IProductNotify subscriber)
        {
            this.QueryReportWorker.Subscribers.Add(subscriber);

        }
    }
}
