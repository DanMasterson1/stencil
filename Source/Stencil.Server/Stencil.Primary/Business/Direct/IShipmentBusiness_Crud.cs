using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IShipmentBusiness
    {
        Shipment GetById(Guid shipment_id);
        
        List<Shipment> GetByOrderId(Guid order_id);
        void InvalidateForOrderId(Guid order_id, string reason);Shipment Insert(Shipment insertShipment);
        Shipment Update(Shipment updateShipment);
        
        void Delete(Guid shipment_id);
        void SynchronizationUpdate(Guid shipment_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid shipment_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid shipment_id, string reason);
        
    }
}

