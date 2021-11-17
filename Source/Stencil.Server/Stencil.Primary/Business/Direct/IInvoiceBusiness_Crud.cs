using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IInvoiceBusiness
    {
        Invoice GetById(Guid invoice_id);
        
        List<Invoice> GetByOrderId(Guid order_id);
        void InvalidateForOrderId(Guid order_id, string reason);
        List<Invoice> GetByAssetId(Guid asset_id);
        void InvalidateForAssetId(Guid asset_id, string reason);Invoice Insert(Invoice insertInvoice);
        Invoice Update(Invoice updateInvoice);
        
        void Delete(Guid invoice_id);
        void SynchronizationUpdate(Guid invoice_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid invoice_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid invoice_id, string reason);
        
    }
}

