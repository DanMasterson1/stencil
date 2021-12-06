using am = AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Data.Sql;
using Stencil.Domain;

namespace Stencil.Primary.Mapping
{
    public partial class PrimaryMappingProfile : AutoMapper.Profile
    {
        public PrimaryMappingProfile()
            : base("PrimaryMappingProfile")
        {
        }

        protected override void Configure()
        {
            this.DbAndDomainMappings();
            this.DomainAndSDKMappings();
            
            this.DbAndDomainMappings_Manual();
            this.DomainAndSDKMappings_Manual();
        }
        
        partial void DbAndDomainMappings_Manual();
        partial void DomainAndSDKMappings_Manual();
        
        protected void DbAndDomainMappings()
        {
            am.Mapper.CreateMap<DateTimeOffset?, DateTime?>()
                .ConvertUsing(x => x.HasValue ? x.Value.UtcDateTime : (DateTime?)null);

            am.Mapper.CreateMap<DateTimeOffset, DateTime?>()
                .ConvertUsing(x => x.UtcDateTime);

            am.Mapper.CreateMap<DateTimeOffset, DateTime>()
                .ConvertUsing(x => x.UtcDateTime);

            am.Mapper.CreateMap<DateTime?, DateTimeOffset?>()
                .ConvertUsing(x => x.HasValue ? new DateTimeOffset(x.Value) : (DateTimeOffset?)null);
                
            am.Mapper.CreateMap<dbGlobalSetting, GlobalSetting>();
            am.Mapper.CreateMap<GlobalSetting, dbGlobalSetting>();
            am.Mapper.CreateMap<dbBrand, Brand>();
            am.Mapper.CreateMap<Brand, dbBrand>();
            am.Mapper.CreateMap<dbProduct, Product>();
            am.Mapper.CreateMap<Product, dbProduct>();
            am.Mapper.CreateMap<dbPromotion, Promotion>();
            am.Mapper.CreateMap<Promotion, dbPromotion>();
            am.Mapper.CreateMap<dbListing, Listing>();
            am.Mapper.CreateMap<Listing, dbListing>();
            am.Mapper.CreateMap<dbOrder, Order>();
            am.Mapper.CreateMap<Order, dbOrder>();
            am.Mapper.CreateMap<dbLineItem, LineItem>();
            am.Mapper.CreateMap<LineItem, dbLineItem>();
            am.Mapper.CreateMap<dbInvoice, Invoice>();
            am.Mapper.CreateMap<Invoice, dbInvoice>();
            am.Mapper.CreateMap<dbPayment, Payment>();
            am.Mapper.CreateMap<Payment, dbPayment>();
            am.Mapper.CreateMap<dbShipment, Shipment>();
            am.Mapper.CreateMap<Shipment, dbShipment>();
            am.Mapper.CreateMap<dbPaymentTransaction, PaymentTransaction>();
            am.Mapper.CreateMap<PaymentTransaction, dbPaymentTransaction>();
            am.Mapper.CreateMap<dbPaymentDetail, PaymentDetail>();
            am.Mapper.CreateMap<PaymentDetail, dbPaymentDetail>();
            am.Mapper.CreateMap<dbSubscription, Subscription>();
            am.Mapper.CreateMap<Subscription, dbSubscription>();
            am.Mapper.CreateMap<dbAccount, Account>();
            am.Mapper.CreateMap<Account, dbAccount>();
            am.Mapper.CreateMap<dbAsset, Asset>();
            am.Mapper.CreateMap<Asset, dbAsset>();
            
        }
        protected void DomainAndSDKMappings()
        {
            am.Mapper.CreateMap<Domain.AssetType, SDK.Models.AssetType>().ConvertUsing(x => (SDK.Models.AssetType)(int)x);
            am.Mapper.CreateMap<SDK.Models.AssetType, Domain.AssetType>().ConvertUsing(x => (Domain.AssetType)(int)x);
            am.Mapper.CreateMap<Domain.OrderStatus, SDK.Models.OrderStatus>().ConvertUsing(x => (SDK.Models.OrderStatus)(int)x);
            am.Mapper.CreateMap<SDK.Models.OrderStatus, Domain.OrderStatus>().ConvertUsing(x => (Domain.OrderStatus)(int)x);
            am.Mapper.CreateMap<Domain.CardType, SDK.Models.CardType>().ConvertUsing(x => (SDK.Models.CardType)(int)x);
            am.Mapper.CreateMap<SDK.Models.CardType, Domain.CardType>().ConvertUsing(x => (Domain.CardType)(int)x);
            am.Mapper.CreateMap<Domain.PromotionType, SDK.Models.PromotionType>().ConvertUsing(x => (SDK.Models.PromotionType)(int)x);
            am.Mapper.CreateMap<SDK.Models.PromotionType, Domain.PromotionType>().ConvertUsing(x => (Domain.PromotionType)(int)x);
            am.Mapper.CreateMap<Domain.CarrierType, SDK.Models.CarrierType>().ConvertUsing(x => (SDK.Models.CarrierType)(int)x);
            am.Mapper.CreateMap<SDK.Models.CarrierType, Domain.CarrierType>().ConvertUsing(x => (Domain.CarrierType)(int)x);
            am.Mapper.CreateMap<Domain.TransactionOutcome, SDK.Models.TransactionOutcome>().ConvertUsing(x => (SDK.Models.TransactionOutcome)(int)x);
            am.Mapper.CreateMap<SDK.Models.TransactionOutcome, Domain.TransactionOutcome>().ConvertUsing(x => (Domain.TransactionOutcome)(int)x);
            am.Mapper.CreateMap<Domain.Dependency, SDK.Models.Dependency>().ConvertUsing(x => (SDK.Models.Dependency)(int)x);
            am.Mapper.CreateMap<SDK.Models.Dependency, Domain.Dependency>().ConvertUsing(x => (Domain.Dependency)(int)x);
            
            am.Mapper.CreateMap<Domain.GlobalSetting, SDK.Models.GlobalSetting>();
            am.Mapper.CreateMap<SDK.Models.GlobalSetting, Domain.GlobalSetting>();
            
            am.Mapper.CreateMap<Domain.Brand, SDK.Models.Brand>();
            am.Mapper.CreateMap<SDK.Models.Brand, Domain.Brand>();
            
            am.Mapper.CreateMap<Domain.Product, SDK.Models.Product>();
            am.Mapper.CreateMap<SDK.Models.Product, Domain.Product>();
            
            am.Mapper.CreateMap<Domain.Promotion, SDK.Models.Promotion>();
            am.Mapper.CreateMap<SDK.Models.Promotion, Domain.Promotion>();
            
            am.Mapper.CreateMap<Domain.Listing, SDK.Models.Listing>();
            am.Mapper.CreateMap<SDK.Models.Listing, Domain.Listing>();
            
            am.Mapper.CreateMap<Domain.Order, SDK.Models.Order>();
            am.Mapper.CreateMap<SDK.Models.Order, Domain.Order>();
            
            am.Mapper.CreateMap<Domain.LineItem, SDK.Models.LineItem>();
            am.Mapper.CreateMap<SDK.Models.LineItem, Domain.LineItem>();
            
            am.Mapper.CreateMap<Domain.Invoice, SDK.Models.Invoice>();
            am.Mapper.CreateMap<SDK.Models.Invoice, Domain.Invoice>();
            
            am.Mapper.CreateMap<Domain.Payment, SDK.Models.Payment>();
            am.Mapper.CreateMap<SDK.Models.Payment, Domain.Payment>();
            
            am.Mapper.CreateMap<Domain.Shipment, SDK.Models.Shipment>();
            am.Mapper.CreateMap<SDK.Models.Shipment, Domain.Shipment>();
            
            am.Mapper.CreateMap<Domain.PaymentTransaction, SDK.Models.PaymentTransaction>();
            am.Mapper.CreateMap<SDK.Models.PaymentTransaction, Domain.PaymentTransaction>();
            
            am.Mapper.CreateMap<Domain.PaymentDetail, SDK.Models.PaymentDetail>();
            am.Mapper.CreateMap<SDK.Models.PaymentDetail, Domain.PaymentDetail>();
            
            am.Mapper.CreateMap<Domain.Subscription, SDK.Models.Subscription>();
            am.Mapper.CreateMap<SDK.Models.Subscription, Domain.Subscription>();
            
            am.Mapper.CreateMap<Domain.Account, SDK.Models.Account>();
            am.Mapper.CreateMap<SDK.Models.Account, Domain.Account>();
            
            am.Mapper.CreateMap<Domain.Asset, SDK.Models.Asset>();
            am.Mapper.CreateMap<SDK.Models.Asset, Domain.Asset>();
            
        }
    }
}

