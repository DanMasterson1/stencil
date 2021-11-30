using System;
using System.Net.Http;
using System.Text;
using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Domain;
using Stencil.Plugins.OrderInformant.Models;

namespace Stencil.Plugins.OrderInformant.Integration
{
    public class ProcessNotify : ChokeableClass, IProcessNotification
    {
        public ProcessNotify(IFoundation foundation)
        : base(foundation)
        {
        }

        public void Send(OrderNotification notification)
        {
            base.ExecuteMethod(nameof(Send), delegate ()
            {
                HttpClient client = new HttpClient();

                foreach (Subscription subscription in notification.subscriptions)
                {
                    var data = new StringContent(notification.payload, Encoding.UTF8, "application/json");
                    HttpResponseMessage response =  client.PostAsync(new Uri(subscription.url), data).Result;
                }
            });

        }
    }
}