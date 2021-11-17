using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IPaymentTransactionBusiness
    {
        PaymentTransaction GetById(Guid paymenttransaction_id);
        
        List<PaymentTransaction> GetByOrderId(Guid order_id);
        void InvalidateForOrderId(Guid order_id, string reason);
        List<PaymentTransaction> GetByPaymentId(Guid payment_id);
        void InvalidateForPaymentId(Guid payment_id, string reason);PaymentTransaction Insert(PaymentTransaction insertPaymentTransaction);
        PaymentTransaction Update(PaymentTransaction updatePaymentTransaction);
        
        void Delete(Guid paymenttransaction_id);
        void SynchronizationUpdate(Guid paymenttransaction_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid paymenttransaction_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid paymenttransaction_id, string reason);
        
    }
}

