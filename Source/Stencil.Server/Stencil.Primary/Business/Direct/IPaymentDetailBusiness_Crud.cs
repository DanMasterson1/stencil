using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IPaymentDetailBusiness
    {
        PaymentDetail GetById(Guid paymentdetail_id);
        
        List<PaymentDetail> GetByAccountId(Guid account_id);
        void InvalidateForAccountId(Guid account_id, string reason);PaymentDetail Insert(PaymentDetail insertPaymentDetail);
        PaymentDetail Update(PaymentDetail updatePaymentDetail);
        
        void Delete(Guid paymentdetail_id);
        void SynchronizationUpdate(Guid paymentdetail_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid paymentdetail_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid paymentdetail_id, string reason);
        
    }
}

