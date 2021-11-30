using System.Collections.Generic;
using Stencil.Domain;

namespace Stencil.Plugins.ProductInformant.Models
{
    public class ProductNotification
    {
        public List<Subscription> subscriptions;
        public string payload;
    }
}