using Codeable.Foundation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Primary.Business.Index;

namespace Stencil.Primary
{
    public class StencilAPIIndex : BaseClass
    {
        public StencilAPIIndex(IFoundation ifoundation)
            : base(ifoundation)
        {
        }

        public IBrandIndex Brands
        {
            get { return this.IFoundation.Resolve<IBrandIndex>(); }
        }
        public IProductIndex Products
        {
            get { return this.IFoundation.Resolve<IProductIndex>(); }
        }
        public IPromotionIndex Promotions
        {
            get { return this.IFoundation.Resolve<IPromotionIndex>(); }
        }
        public IListingIndex Listings
        {
            get { return this.IFoundation.Resolve<IListingIndex>(); }
        }
        public IOrderIndex Orders
        {
            get { return this.IFoundation.Resolve<IOrderIndex>(); }
        }
        public ILineItemIndex LineItems
        {
            get { return this.IFoundation.Resolve<ILineItemIndex>(); }
        }
        public IInvoiceIndex Invoices
        {
            get { return this.IFoundation.Resolve<IInvoiceIndex>(); }
        }
        public IPaymentIndex Payments
        {
            get { return this.IFoundation.Resolve<IPaymentIndex>(); }
        }
        public IShipmentIndex Shipments
        {
            get { return this.IFoundation.Resolve<IShipmentIndex>(); }
        }
        public IPaymentTransactionIndex PaymentTransactions
        {
            get { return this.IFoundation.Resolve<IPaymentTransactionIndex>(); }
        }
        public IPaymentDetailIndex PaymentDetails
        {
            get { return this.IFoundation.Resolve<IPaymentDetailIndex>(); }
        }
        public IAccountIndex Accounts
        {
            get { return this.IFoundation.Resolve<IAccountIndex>(); }
        }
        
    }
}


