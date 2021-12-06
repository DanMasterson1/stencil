using System;
using System.Net.Http;
using System.Text;
using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Domain;
using Stencil.Plugins.ProductInformant.Models;

namespace Stencil.Plugins.ProductInformant.Integration
{
    public class HttpDispatcher : ChokeableClass, IDispatchNotifications
    {
        public HttpDispatcher(IFoundation foundation)
        : base(foundation)
        {
        }

        public void Dispatch(ProductNotification notification)
        {
            base.ExecuteMethod(nameof(Dispatch), delegate ()
            {
                HttpClient client = new HttpClient();

                foreach (Subscription subscription in notification.subscriptions)
                {
                    var data = new StringContent(notification.payload, Encoding.UTF8, "application/json");
                    client.PostAsync(new Uri(subscription.url), data);
                }
            });

        }
    }
}