using Stencil.SDK.Endpoints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK
{
    public partial class StencilSDK
    {
        // members for web ease
        public GlobalSettingEndpoint GlobalSetting;
        public BrandEndpoint Brand;
        public ProductEndpoint Product;
        public PromotionEndpoint Promotion;
        public ListingEndpoint Listing;
        public OrderEndpoint Order;
        public LineItemEndpoint LineItem;
        public InvoiceEndpoint Invoice;
        public PaymentEndpoint Payment;
        public ShipmentEndpoint Shipment;
        public PaymentTransactionEndpoint PaymentTransaction;
        public PaymentDetailEndpoint PaymentDetail;
        public SubscriptionEndpoint Subscription;
        public AccountEndpoint Account;
        public AssetEndpoint Asset;
        

        protected virtual void ConstructCoreEndpoints()
        {
            this.GlobalSetting = new GlobalSettingEndpoint(this);
            this.Brand = new BrandEndpoint(this);
            this.Product = new ProductEndpoint(this);
            this.Promotion = new PromotionEndpoint(this);
            this.Listing = new ListingEndpoint(this);
            this.Order = new OrderEndpoint(this);
            this.LineItem = new LineItemEndpoint(this);
            this.Invoice = new InvoiceEndpoint(this);
            this.Payment = new PaymentEndpoint(this);
            this.Shipment = new ShipmentEndpoint(this);
            this.PaymentTransaction = new PaymentTransactionEndpoint(this);
            this.PaymentDetail = new PaymentDetailEndpoint(this);
            this.Subscription = new SubscriptionEndpoint(this);
            this.Account = new AccountEndpoint(this);
            this.Asset = new AssetEndpoint(this);
            
        }   
    }
}

