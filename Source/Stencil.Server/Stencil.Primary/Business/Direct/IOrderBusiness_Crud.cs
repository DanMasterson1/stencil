using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IOrderBusiness
    {
        Order GetById(Guid order_id);
        
        List<Order> GetByAccountId(Guid account_id);
        void InvalidateForAccountId(Guid account_id, string reason);
        List<Order> GetByInvoiceId(Guid invoice_id);
        
        List<Order> GetByPaymentId(Guid payment_id);
        void InvalidateForPaymentId(Guid payment_id, string reason);
        List<Order> GetByShipmentId(Guid shipment_id);
        void InvalidateForShipmentId(Guid shipment_id, string reason);Order Insert(Order insertOrder);
        Order Update(Order updateOrder);
        
        void Delete(Guid order_id);
        void SynchronizationUpdate(Guid order_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid order_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid order_id, string reason);
        
    }
}

