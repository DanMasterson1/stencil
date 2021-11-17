using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IPaymentBusiness
    {
        Payment GetById(Guid payment_id);
        
        List<Payment> GetByOrderId(Guid order_id);
        void InvalidateForOrderId(Guid order_id, string reason);Payment Insert(Payment insertPayment);
        Payment Update(Payment updatePayment);
        
        void Delete(Guid payment_id);
        void SynchronizationUpdate(Guid payment_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid payment_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid payment_id, string reason);
        
    }
}

