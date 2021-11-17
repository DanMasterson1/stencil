using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IListingBusiness
    {
        Listing GetById(Guid listing_id);
        List<Listing> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false);
        
        List<Listing> GetByBrandId(Guid brand_id);
        void InvalidateForBrandId(Guid brand_id, string reason);
        List<Listing> GetByProductId(Guid product_id);
        void InvalidateForProductId(Guid product_id, string reason);
        List<Listing> GetByPromotionId(Guid promotion_id);
        void InvalidateForPromotionId(Guid promotion_id, string reason);Listing Insert(Listing insertListing);
        Listing Update(Listing updateListing);
        
        void Delete(Guid listing_id);
        void SynchronizationUpdate(Guid listing_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid listing_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid listing_id, string reason);
        
    }
}

