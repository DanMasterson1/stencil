using Codeable.Foundation.Common;
using Newtonsoft.Json;
using Stencil.Primary.Daemons;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Stencil.Primary.Models;
using Stencil.Primary.Integration;
using System.Collections.Generic;

namespace Stencil.Primary.Workers
{
    public class QueryReportWorker : WorkerBase<QueryReportRequest>, IQueryReportWorker
    {
        public const string WORKER_NAME = nameof(QueryReportWorker);
        public StencilAPI API { get; set; }
        public List<IProductNotify> Subscribers { get; private set; }

        public QueryReportWorker(IFoundation iFoundation)
            : base(iFoundation, WORKER_NAME)
        {
            this.API = iFoundation.Resolve<StencilAPI>();
            this.Subscribers = new List<IProductNotify>();
        }

        public static void EnqueueRequest(IFoundation foundation, QueryReportRequest request)
        {
            EnqueueRequest<QueryReportWorker>(foundation, WORKER_NAME, request, (int)TimeSpan.FromMinutes(2).TotalMilliseconds); // updates every 2 mins
        }

        protected override void ProcessRequest(QueryReportRequest request)
        {
            base.ExecuteMethod(nameof(ProcessRequest), delegate ()
            {
                ReportRequestAsync(request);

            });
        }

        // I need to call the webhook notify
        private async Task ReportRequestAsync(QueryReportRequest request)
        {
            base.ExecuteMethod(nameof(ReportRequestAsync), delegate ()
            {
                foreach (IProductNotify subscriber in this.Subscribers)
                {
                    //   // await subscriber.Notify();
                }
            });
         
            // maybe this needs to send a post to the plugins o

            //INotify notify;
            // would look through the list of subscribers

            //NotifyController notify = new NotifyController();
          

            //HttpClient client = new HttpClient();

            //var data = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            //var response = client.PostAsync(new Uri("https://postb.in/1636733664035-1320111758541"), data).Result;

        }
        // exposes interface to get on their registered list
        // add method exposed


    }
}
