using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IBrandBusiness
    {
        Brand GetById(Guid brand_id);
        List<Brand> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false);
        Brand Insert(Brand insertBrand);
        Brand Update(Brand updateBrand);
        
        void Delete(Guid brand_id);
        void SynchronizationUpdate(Guid brand_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid brand_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid brand_id, string reason);
        
    }
}

