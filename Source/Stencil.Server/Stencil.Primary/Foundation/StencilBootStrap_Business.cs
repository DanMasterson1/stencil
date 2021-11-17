using Codeable.Foundation.Common;
using Codeable.Foundation.UI.Web.Core.Unity;
using Stencil.Primary.Business.Direct;
using Stencil.Primary.Business.Direct.Implementation;
using Stencil.Primary.Business.Index;
using Stencil.Primary.Business.Index.Implementation;
using Stencil.Primary.Synchronization;
using Stencil.Primary.Synchronization.Implementation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Foundation
{
    public partial class StencilBootStrap
    {
        protected virtual void RegisterDataElements(IFoundation foundation)
        {
            foundation.Container.RegisterType<IGlobalSettingBusiness, GlobalSettingBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IBrandBusiness, BrandBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IProductBusiness, ProductBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPromotionBusiness, PromotionBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IListingBusiness, ListingBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IOrderBusiness, OrderBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<ILineItemBusiness, LineItemBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IInvoiceBusiness, InvoiceBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentBusiness, PaymentBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IShipmentBusiness, ShipmentBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentTransactionBusiness, PaymentTransactionBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentDetailBusiness, PaymentDetailBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<ISubscriptionBusiness, SubscriptionBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IAccountBusiness, AccountBusiness>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IAssetBusiness, AssetBusiness>(new HttpRequestLifetimeManager());
            
            
            //Indexes
            foundation.Container.RegisterType<IBrandIndex, BrandIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IProductIndex, ProductIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPromotionIndex, PromotionIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IListingIndex, ListingIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IOrderIndex, OrderIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<ILineItemIndex, LineItemIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IInvoiceIndex, InvoiceIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentIndex, PaymentIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IShipmentIndex, ShipmentIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentTransactionIndex, PaymentTransactionIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentDetailIndex, PaymentDetailIndex>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IAccountIndex, AccountIndex>(new HttpRequestLifetimeManager());
            
            
            //Synchronizers
            foundation.Container.RegisterType<IBrandSynchronizer, BrandSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IProductSynchronizer, ProductSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPromotionSynchronizer, PromotionSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IListingSynchronizer, ListingSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IOrderSynchronizer, OrderSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<ILineItemSynchronizer, LineItemSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IInvoiceSynchronizer, InvoiceSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentSynchronizer, PaymentSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IShipmentSynchronizer, ShipmentSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentTransactionSynchronizer, PaymentTransactionSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IPaymentDetailSynchronizer, PaymentDetailSynchronizer>(new HttpRequestLifetimeManager());
            foundation.Container.RegisterType<IAccountSynchronizer, AccountSynchronizer>(new HttpRequestLifetimeManager());
            
        }
    }
}

