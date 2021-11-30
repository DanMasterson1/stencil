using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Domain;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Common;

namespace Stencil.Primary.Business.Integration
{
    public partial class DependencyCoordinator_Core : ChokeableClass, IDependencyCoordinator
    {
        public DependencyCoordinator_Core(IFoundation iFoundation)
            : base(iFoundation)
        {
            this.API = new StencilAPI(iFoundation);
        }
        public virtual StencilAPI API { get; set; }
        
        public virtual void GlobalSettingInvalidated(Dependency affectedDependencies, Guid global_setting_id)
        {
            base.ExecuteMethod("GlobalSettingInvalidated", delegate ()
            {
                DependencyWorker<GlobalSetting>.EnqueueRequest(this.IFoundation, affectedDependencies, global_setting_id, this.ProcessGlobalSettingInvalidation);
            });
        }
        protected virtual void ProcessGlobalSettingInvalidation(Dependency dependencies, Guid global_setting_id)
        {
            base.ExecuteMethod("ProcessGlobalSettingInvalidation", delegate ()
            {
                GlobalSetting item = this.API.Direct.GlobalSettings.GetById(global_setting_id);
                
            });
        }
        public virtual void BrandInvalidated(Dependency affectedDependencies, Guid brand_id)
        {
            base.ExecuteMethod("BrandInvalidated", delegate ()
            {
                DependencyWorker<Brand>.EnqueueRequest(this.IFoundation, affectedDependencies, brand_id, this.ProcessBrandInvalidation);
            });
        }
        protected virtual void ProcessBrandInvalidation(Dependency dependencies, Guid brand_id)
        {
            base.ExecuteMethod("ProcessBrandInvalidation", delegate ()
            {
                
				
                this.API.Direct.Products.InvalidateForBrandId(brand_id, " changed");
                
				
                this.API.Direct.Listings.InvalidateForBrandId(brand_id, " changed");
                Brand item = this.API.Direct.Brands.GetById(brand_id);
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void ProductInvalidated(Dependency affectedDependencies, Guid product_id)
        {
            base.ExecuteMethod("ProductInvalidated", delegate ()
            {
                DependencyWorker<Product>.EnqueueRequest(this.IFoundation, affectedDependencies, product_id, this.ProcessProductInvalidation);
            });
        }
        protected virtual void ProcessProductInvalidation(Dependency dependencies, Guid product_id)
        {
            base.ExecuteMethod("ProcessProductInvalidation", delegate ()
            {
                
				
                this.API.Direct.Listings.InvalidateForProductId(product_id, " changed");
                Product item = this.API.Direct.Products.GetById(product_id);
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void PromotionInvalidated(Dependency affectedDependencies, Guid promotion_id)
        {
            base.ExecuteMethod("PromotionInvalidated", delegate ()
            {
                DependencyWorker<Promotion>.EnqueueRequest(this.IFoundation, affectedDependencies, promotion_id, this.ProcessPromotionInvalidation);
            });
        }
        protected virtual void ProcessPromotionInvalidation(Dependency dependencies, Guid promotion_id)
        {
            base.ExecuteMethod("ProcessPromotionInvalidation", delegate ()
            {
                
				
                this.API.Direct.Listings.InvalidateForPromotionId(promotion_id, " changed");
                Promotion item = this.API.Direct.Promotions.GetById(promotion_id);
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void ListingInvalidated(Dependency affectedDependencies, Guid listing_id)
        {
            base.ExecuteMethod("ListingInvalidated", delegate ()
            {
                DependencyWorker<Listing>.EnqueueRequest(this.IFoundation, affectedDependencies, listing_id, this.ProcessListingInvalidation);
            });
        }
        protected virtual void ProcessListingInvalidation(Dependency dependencies, Guid listing_id)
        {
            base.ExecuteMethod("ProcessListingInvalidation", delegate ()
            {
                
				
                this.API.Direct.LineItems.InvalidateForListingId(listing_id, " changed");
                Listing item = this.API.Direct.Listings.GetById(listing_id);
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void OrderInvalidated(Dependency affectedDependencies, Guid order_id)
        {
            base.ExecuteMethod("OrderInvalidated", delegate ()
            {
                DependencyWorker<Order>.EnqueueRequest(this.IFoundation, affectedDependencies, order_id, this.ProcessOrderInvalidation);
            });
        }
        protected virtual void ProcessOrderInvalidation(Dependency dependencies, Guid order_id)
        {
            base.ExecuteMethod("ProcessOrderInvalidation", delegate ()
            {
                
				
                this.API.Direct.LineItems.InvalidateForOrderId(order_id, " changed");
                
				
                this.API.Direct.Invoices.InvalidateForOrderId(order_id, " changed");
                
				
                this.API.Direct.Payments.InvalidateForOrderId(order_id, " changed");
                
				
                this.API.Direct.Shipments.InvalidateForOrderId(order_id, " changed");
                
				
                this.API.Direct.PaymentTransactions.InvalidateForOrderId(order_id, " changed");
                Order item = this.API.Direct.Orders.GetById(order_id);
                
                
                if (item != null && item.invoice_id.HasValue)
                {
                    this.API.Direct.Invoices.Invalidate(item.invoice_id.Value, "Order changed");
                }
                
                
                if (item != null && item.payment_id.HasValue)
                {
                    this.API.Direct.Payments.Invalidate(item.payment_id.Value, "Order changed");
                }
                
                
                if (item != null && item.shipment_id.HasValue)
                {
                    this.API.Direct.Shipments.Invalidate(item.shipment_id.Value, "Order changed");
                }
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void LineItemInvalidated(Dependency affectedDependencies, Guid lineitem_id)
        {
            base.ExecuteMethod("LineItemInvalidated", delegate ()
            {
                DependencyWorker<LineItem>.EnqueueRequest(this.IFoundation, affectedDependencies, lineitem_id, this.ProcessLineItemInvalidation);
            });
        }
        protected virtual void ProcessLineItemInvalidation(Dependency dependencies, Guid lineitem_id)
        {
            base.ExecuteMethod("ProcessLineItemInvalidation", delegate ()
            {
                LineItem item = this.API.Direct.LineItems.GetById(lineitem_id);
                
                
                if (item != null)
                {
                    this.API.Direct.Orders.Invalidate(item.order_id, "LineItem changed");
                }
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void InvoiceInvalidated(Dependency affectedDependencies, Guid invoice_id)
        {
            base.ExecuteMethod("InvoiceInvalidated", delegate ()
            {
                DependencyWorker<Invoice>.EnqueueRequest(this.IFoundation, affectedDependencies, invoice_id, this.ProcessInvoiceInvalidation);
            });
        }
        protected virtual void ProcessInvoiceInvalidation(Dependency dependencies, Guid invoice_id)
        {
            base.ExecuteMethod("ProcessInvoiceInvalidation", delegate ()
            {
                Invoice item = this.API.Direct.Invoices.GetById(invoice_id);
                
                
                if (item != null)
                {
                    this.API.Direct.Orders.Invalidate(item.order_id, "Invoice changed");
                }
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void PaymentInvalidated(Dependency affectedDependencies, Guid payment_id)
        {
            base.ExecuteMethod("PaymentInvalidated", delegate ()
            {
                DependencyWorker<Payment>.EnqueueRequest(this.IFoundation, affectedDependencies, payment_id, this.ProcessPaymentInvalidation);
            });
        }
        protected virtual void ProcessPaymentInvalidation(Dependency dependencies, Guid payment_id)
        {
            base.ExecuteMethod("ProcessPaymentInvalidation", delegate ()
            {
                
				
                this.API.Direct.Orders.InvalidateForPaymentId(payment_id, " changed");
                
				
                this.API.Direct.PaymentTransactions.InvalidateForPaymentId(payment_id, " changed");
                Payment item = this.API.Direct.Payments.GetById(payment_id);
                
                
                if (item != null)
                {
                    this.API.Direct.Orders.Invalidate(item.order_id, "Payment changed");
                }
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void ShipmentInvalidated(Dependency affectedDependencies, Guid shipment_id)
        {
            base.ExecuteMethod("ShipmentInvalidated", delegate ()
            {
                DependencyWorker<Shipment>.EnqueueRequest(this.IFoundation, affectedDependencies, shipment_id, this.ProcessShipmentInvalidation);
            });
        }
        protected virtual void ProcessShipmentInvalidation(Dependency dependencies, Guid shipment_id)
        {
            base.ExecuteMethod("ProcessShipmentInvalidation", delegate ()
            {
                
				
                this.API.Direct.Orders.InvalidateForShipmentId(shipment_id, " changed");
                Shipment item = this.API.Direct.Shipments.GetById(shipment_id);
                
                
                if (item != null)
                {
                    this.API.Direct.Orders.Invalidate(item.order_id, "Shipment changed");
                }
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void PaymentTransactionInvalidated(Dependency affectedDependencies, Guid paymenttransaction_id)
        {
            base.ExecuteMethod("PaymentTransactionInvalidated", delegate ()
            {
                DependencyWorker<PaymentTransaction>.EnqueueRequest(this.IFoundation, affectedDependencies, paymenttransaction_id, this.ProcessPaymentTransactionInvalidation);
            });
        }
        protected virtual void ProcessPaymentTransactionInvalidation(Dependency dependencies, Guid paymenttransaction_id)
        {
            base.ExecuteMethod("ProcessPaymentTransactionInvalidation", delegate ()
            {
                PaymentTransaction item = this.API.Direct.PaymentTransactions.GetById(paymenttransaction_id);
                
                
                if (item != null)
                {
                    this.API.Direct.Orders.Invalidate(item.order_id, "PaymentTransaction changed");
                }
                
                
                if (item != null)
                {
                    this.API.Direct.Payments.Invalidate(item.payment_id, "PaymentTransaction changed");
                }
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void PaymentDetailInvalidated(Dependency affectedDependencies, Guid paymentdetail_id)
        {
            base.ExecuteMethod("PaymentDetailInvalidated", delegate ()
            {
                DependencyWorker<PaymentDetail>.EnqueueRequest(this.IFoundation, affectedDependencies, paymentdetail_id, this.ProcessPaymentDetailInvalidation);
            });
        }
        protected virtual void ProcessPaymentDetailInvalidation(Dependency dependencies, Guid paymentdetail_id)
        {
            base.ExecuteMethod("ProcessPaymentDetailInvalidation", delegate ()
            {
                PaymentDetail item = this.API.Direct.PaymentDetails.GetById(paymentdetail_id);
                
                
                if (item != null)
                {
                    this.API.Direct.Accounts.Invalidate(item.account_id, "PaymentDetail changed");
                }
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void SubscriptionInvalidated(Dependency affectedDependencies, Guid subscription_id)
        {
            base.ExecuteMethod("SubscriptionInvalidated", delegate ()
            {
                DependencyWorker<Subscription>.EnqueueRequest(this.IFoundation, affectedDependencies, subscription_id, this.ProcessSubscriptionInvalidation);
            });
        }
        protected virtual void ProcessSubscriptionInvalidation(Dependency dependencies, Guid subscription_id)
        {
            base.ExecuteMethod("ProcessSubscriptionInvalidation", delegate ()
            {
                Subscription item = this.API.Direct.Subscriptions.GetById(subscription_id);
                
            });
        }
        public virtual void AccountInvalidated(Dependency affectedDependencies, Guid account_id)
        {
            base.ExecuteMethod("AccountInvalidated", delegate ()
            {
                DependencyWorker<Account>.EnqueueRequest(this.IFoundation, affectedDependencies, account_id, this.ProcessAccountInvalidation);
            });
        }
        protected virtual void ProcessAccountInvalidation(Dependency dependencies, Guid account_id)
        {
            base.ExecuteMethod("ProcessAccountInvalidation", delegate ()
            {
                
				
                this.API.Direct.Orders.InvalidateForAccountId(account_id, " changed");
                
				
                this.API.Direct.PaymentDetails.InvalidateForAccountId(account_id, " changed");
                Account item = this.API.Direct.Accounts.GetById(account_id);
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        public virtual void AssetInvalidated(Dependency affectedDependencies, Guid asset_id)
        {
            base.ExecuteMethod("AssetInvalidated", delegate ()
            {
                DependencyWorker<Asset>.EnqueueRequest(this.IFoundation, affectedDependencies, asset_id, this.ProcessAssetInvalidation);
            });
        }
        protected virtual void ProcessAssetInvalidation(Dependency dependencies, Guid asset_id)
        {
            base.ExecuteMethod("ProcessAssetInvalidation", delegate ()
            {
                
				
                this.API.Direct.Invoices.InvalidateForAssetId(asset_id, " changed");
                Asset item = this.API.Direct.Assets.GetById(asset_id);
                
                this.API.Integration.Synchronization.AgitateSyncDaemon();
            });
        }
        
    }
}


