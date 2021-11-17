using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Domain;

namespace Stencil.Primary.Business.Integration
{
    public interface IDependencyCoordinator
    {
        void GlobalSettingInvalidated(Dependency affectedDependencies, Guid global_setting_id);
        void BrandInvalidated(Dependency affectedDependencies, Guid brand_id);
        void ProductInvalidated(Dependency affectedDependencies, Guid product_id);
        void PromotionInvalidated(Dependency affectedDependencies, Guid promotion_id);
        void ListingInvalidated(Dependency affectedDependencies, Guid listing_id);
        void OrderInvalidated(Dependency affectedDependencies, Guid order_id);
        void LineItemInvalidated(Dependency affectedDependencies, Guid lineitem_id);
        void InvoiceInvalidated(Dependency affectedDependencies, Guid invoice_id);
        void PaymentInvalidated(Dependency affectedDependencies, Guid payment_id);
        void ShipmentInvalidated(Dependency affectedDependencies, Guid shipment_id);
        void PaymentTransactionInvalidated(Dependency affectedDependencies, Guid paymenttransaction_id);
        void PaymentDetailInvalidated(Dependency affectedDependencies, Guid paymentdetail_id);
        void SubscriptionInvalidated(Dependency affectedDependencies, Guid subscription_id);
        void AccountInvalidated(Dependency affectedDependencies, Guid account_id);
        void AssetInvalidated(Dependency affectedDependencies, Guid asset_id);
        
    }
}


