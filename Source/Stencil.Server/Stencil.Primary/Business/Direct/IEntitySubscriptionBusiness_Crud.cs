using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IEntitySubscriptionBusiness
    {
        EntitySubscription GetById(Guid subscription_id);
        
        List<EntitySubscription> GetByBrandId(Guid brand_id);
        EntitySubscription Insert(EntitySubscription insertEntitySubscription);
        EntitySubscription Update(EntitySubscription updateEntitySubscription);
        
        void Delete(Guid subscription_id);
        
        
    }
}

