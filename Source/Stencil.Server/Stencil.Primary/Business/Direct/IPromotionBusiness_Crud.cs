using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IPromotionBusiness
    {
        Promotion GetById(Guid promotion_id);
        Promotion Insert(Promotion insertPromotion);
        Promotion Update(Promotion updatePromotion);
        
        void Delete(Guid promotion_id);
        void SynchronizationUpdate(Guid promotion_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid promotion_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid promotion_id, string reason);
        
    }
}

