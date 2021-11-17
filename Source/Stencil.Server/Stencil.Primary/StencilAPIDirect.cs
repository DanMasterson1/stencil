using Codeable.Foundation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stencil.Primary.Business.Direct;

namespace Stencil.Primary
{
    public class StencilAPIDirect : BaseClass
    {
        public StencilAPIDirect(IFoundation ifoundation)
            : base(ifoundation)
        {
        }
        public IGlobalSettingBusiness GlobalSettings
        {
            get { return this.IFoundation.Resolve<IGlobalSettingBusiness>(); }
        }
        public IBrandBusiness Brands
        {
            get { return this.IFoundation.Resolve<IBrandBusiness>(); }
        }
        public IProductBusiness Products
        {
            get { return this.IFoundation.Resolve<IProductBusiness>(); }
        }
        public IPromotionBusiness Promotions
        {
            get { return this.IFoundation.Resolve<IPromotionBusiness>(); }
        }
        public IListingBusiness Listings
        {
            get { return this.IFoundation.Resolve<IListingBusiness>(); }
        }
        public IOrderBusiness Orders
        {
            get { return this.IFoundation.Resolve<IOrderBusiness>(); }
        }
        public ILineItemBusiness LineItems
        {
            get { return this.IFoundation.Resolve<ILineItemBusiness>(); }
        }
        public IInvoiceBusiness Invoices
        {
            get { return this.IFoundation.Resolve<IInvoiceBusiness>(); }
        }
        public IPaymentBusiness Payments
        {
            get { return this.IFoundation.Resolve<IPaymentBusiness>(); }
        }
        public IShipmentBusiness Shipments
        {
            get { return this.IFoundation.Resolve<IShipmentBusiness>(); }
        }
        public IPaymentTransactionBusiness PaymentTransactions
        {
            get { return this.IFoundation.Resolve<IPaymentTransactionBusiness>(); }
        }
        public IPaymentDetailBusiness PaymentDetails
        {
            get { return this.IFoundation.Resolve<IPaymentDetailBusiness>(); }
        }
        public ISubscriptionBusiness Subscriptions
        {
            get { return this.IFoundation.Resolve<ISubscriptionBusiness>(); }
        }
        public IAccountBusiness Accounts
        {
            get { return this.IFoundation.Resolve<IAccountBusiness>(); }
        }
        public IAssetBusiness Assets
        {
            get { return this.IFoundation.Resolve<IAssetBusiness>(); }
        }
        
    }
}


