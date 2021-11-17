using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface ILineItemBusiness
    {
        LineItem GetById(Guid lineitem_id);
        
        List<LineItem> GetByOrderId(Guid order_id);
        void InvalidateForOrderId(Guid order_id, string reason);
        List<LineItem> GetByListingId(Guid listing_id);
        void InvalidateForListingId(Guid listing_id, string reason);LineItem Insert(LineItem insertLineItem);
        LineItem Update(LineItem updateLineItem);
        
        void Delete(Guid lineitem_id);
        void SynchronizationUpdate(Guid lineitem_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid lineitem_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid lineitem_id, string reason);
        
    }
}

