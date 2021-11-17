using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface ISubscriptionBusiness
    {
        Subscription GetById(Guid subscription_id);
        
        List<Subscription> GetByBrandId(Guid brand_id);
        
        List<Subscription> GetByProductId(Guid product_id);
        Subscription Insert(Subscription insertSubscription);
        Subscription Update(Subscription updateSubscription);
        
        void Delete(Guid subscription_id);
        
        
    }
}

