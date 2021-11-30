using Stencil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Direct
{
    partial interface ISubscriptionBusiness
    {
        List<Subscription> GetByBrandAndEvent(Guid brand_id, string eventName);

        List<Guid> GetOrderSubscribers();
    }
}
