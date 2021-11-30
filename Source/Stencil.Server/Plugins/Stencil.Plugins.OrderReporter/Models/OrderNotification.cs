using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stencil.Domain;

namespace Stencil.Plugins.OrderInformant.Models
{
    public class OrderNotification
    {
        public List<Subscription> subscriptions;
        public string payload;
    }
}