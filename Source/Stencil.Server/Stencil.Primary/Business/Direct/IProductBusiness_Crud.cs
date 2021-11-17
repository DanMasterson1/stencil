using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    // WARNING: THIS FILE IS GENERATED
    public partial interface IProductBusiness
    {
        Product GetById(Guid product_id);
        List<Product> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false);
        
        List<Product> GetByBrandId(Guid brand_id);
        void InvalidateForBrandId(Guid brand_id, string reason);Product Insert(Product insertProduct);
        Product Update(Product updateProduct);
        
        void Delete(Guid product_id);
        void SynchronizationUpdate(Guid product_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent);
        void SynchronizationHydrateUpdate(Guid product_id, bool success, DateTime sync_date_utc, string sync_log);
        List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent);
        void Invalidate(Guid product_id, string reason);
        
    }
}

