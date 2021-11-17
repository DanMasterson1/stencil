using am = AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Data.Sql;
using Stencil.Domain;

namespace Stencil.Primary
{
    public static partial class _DomainModelExtensions
    {
        
        public static dbGlobalSetting ToDbModel(this GlobalSetting entity, dbGlobalSetting destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbGlobalSetting(); }
                return am.Mapper.Map<GlobalSetting, dbGlobalSetting>(entity, destination);
            }
            return null;
        }
        public static GlobalSetting ToDomainModel(this dbGlobalSetting entity, GlobalSetting destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new GlobalSetting(); }
                return am.Mapper.Map<dbGlobalSetting, GlobalSetting>(entity, destination);
            }
            return null;
        }
        public static List<GlobalSetting> ToDomainModel(this IEnumerable<dbGlobalSetting> entities)
        {
            List<GlobalSetting> result = new List<GlobalSetting>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbBrand ToDbModel(this Brand entity, dbBrand destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbBrand(); }
                return am.Mapper.Map<Brand, dbBrand>(entity, destination);
            }
            return null;
        }
        public static Brand ToDomainModel(this dbBrand entity, Brand destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Brand(); }
                return am.Mapper.Map<dbBrand, Brand>(entity, destination);
            }
            return null;
        }
        public static List<Brand> ToDomainModel(this IEnumerable<dbBrand> entities)
        {
            List<Brand> result = new List<Brand>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbProduct ToDbModel(this Product entity, dbProduct destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbProduct(); }
                return am.Mapper.Map<Product, dbProduct>(entity, destination);
            }
            return null;
        }
        public static Product ToDomainModel(this dbProduct entity, Product destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Product(); }
                return am.Mapper.Map<dbProduct, Product>(entity, destination);
            }
            return null;
        }
        public static List<Product> ToDomainModel(this IEnumerable<dbProduct> entities)
        {
            List<Product> result = new List<Product>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbPromotion ToDbModel(this Promotion entity, dbPromotion destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbPromotion(); }
                return am.Mapper.Map<Promotion, dbPromotion>(entity, destination);
            }
            return null;
        }
        public static Promotion ToDomainModel(this dbPromotion entity, Promotion destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Promotion(); }
                return am.Mapper.Map<dbPromotion, Promotion>(entity, destination);
            }
            return null;
        }
        public static List<Promotion> ToDomainModel(this IEnumerable<dbPromotion> entities)
        {
            List<Promotion> result = new List<Promotion>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbListing ToDbModel(this Listing entity, dbListing destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbListing(); }
                return am.Mapper.Map<Listing, dbListing>(entity, destination);
            }
            return null;
        }
        public static Listing ToDomainModel(this dbListing entity, Listing destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Listing(); }
                return am.Mapper.Map<dbListing, Listing>(entity, destination);
            }
            return null;
        }
        public static List<Listing> ToDomainModel(this IEnumerable<dbListing> entities)
        {
            List<Listing> result = new List<Listing>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbOrder ToDbModel(this Order entity, dbOrder destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbOrder(); }
                return am.Mapper.Map<Order, dbOrder>(entity, destination);
            }
            return null;
        }
        public static Order ToDomainModel(this dbOrder entity, Order destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Order(); }
                return am.Mapper.Map<dbOrder, Order>(entity, destination);
            }
            return null;
        }
        public static List<Order> ToDomainModel(this IEnumerable<dbOrder> entities)
        {
            List<Order> result = new List<Order>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbLineItem ToDbModel(this LineItem entity, dbLineItem destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbLineItem(); }
                return am.Mapper.Map<LineItem, dbLineItem>(entity, destination);
            }
            return null;
        }
        public static LineItem ToDomainModel(this dbLineItem entity, LineItem destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new LineItem(); }
                return am.Mapper.Map<dbLineItem, LineItem>(entity, destination);
            }
            return null;
        }
        public static List<LineItem> ToDomainModel(this IEnumerable<dbLineItem> entities)
        {
            List<LineItem> result = new List<LineItem>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbInvoice ToDbModel(this Invoice entity, dbInvoice destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbInvoice(); }
                return am.Mapper.Map<Invoice, dbInvoice>(entity, destination);
            }
            return null;
        }
        public static Invoice ToDomainModel(this dbInvoice entity, Invoice destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Invoice(); }
                return am.Mapper.Map<dbInvoice, Invoice>(entity, destination);
            }
            return null;
        }
        public static List<Invoice> ToDomainModel(this IEnumerable<dbInvoice> entities)
        {
            List<Invoice> result = new List<Invoice>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbPayment ToDbModel(this Payment entity, dbPayment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbPayment(); }
                return am.Mapper.Map<Payment, dbPayment>(entity, destination);
            }
            return null;
        }
        public static Payment ToDomainModel(this dbPayment entity, Payment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Payment(); }
                return am.Mapper.Map<dbPayment, Payment>(entity, destination);
            }
            return null;
        }
        public static List<Payment> ToDomainModel(this IEnumerable<dbPayment> entities)
        {
            List<Payment> result = new List<Payment>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbShipment ToDbModel(this Shipment entity, dbShipment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbShipment(); }
                return am.Mapper.Map<Shipment, dbShipment>(entity, destination);
            }
            return null;
        }
        public static Shipment ToDomainModel(this dbShipment entity, Shipment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Shipment(); }
                return am.Mapper.Map<dbShipment, Shipment>(entity, destination);
            }
            return null;
        }
        public static List<Shipment> ToDomainModel(this IEnumerable<dbShipment> entities)
        {
            List<Shipment> result = new List<Shipment>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbPaymentTransaction ToDbModel(this PaymentTransaction entity, dbPaymentTransaction destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbPaymentTransaction(); }
                return am.Mapper.Map<PaymentTransaction, dbPaymentTransaction>(entity, destination);
            }
            return null;
        }
        public static PaymentTransaction ToDomainModel(this dbPaymentTransaction entity, PaymentTransaction destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new PaymentTransaction(); }
                return am.Mapper.Map<dbPaymentTransaction, PaymentTransaction>(entity, destination);
            }
            return null;
        }
        public static List<PaymentTransaction> ToDomainModel(this IEnumerable<dbPaymentTransaction> entities)
        {
            List<PaymentTransaction> result = new List<PaymentTransaction>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbPaymentDetail ToDbModel(this PaymentDetail entity, dbPaymentDetail destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbPaymentDetail(); }
                return am.Mapper.Map<PaymentDetail, dbPaymentDetail>(entity, destination);
            }
            return null;
        }
        public static PaymentDetail ToDomainModel(this dbPaymentDetail entity, PaymentDetail destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new PaymentDetail(); }
                return am.Mapper.Map<dbPaymentDetail, PaymentDetail>(entity, destination);
            }
            return null;
        }
        public static List<PaymentDetail> ToDomainModel(this IEnumerable<dbPaymentDetail> entities)
        {
            List<PaymentDetail> result = new List<PaymentDetail>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbSubscription ToDbModel(this Subscription entity, dbSubscription destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbSubscription(); }
                return am.Mapper.Map<Subscription, dbSubscription>(entity, destination);
            }
            return null;
        }
        public static Subscription ToDomainModel(this dbSubscription entity, Subscription destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Subscription(); }
                return am.Mapper.Map<dbSubscription, Subscription>(entity, destination);
            }
            return null;
        }
        public static List<Subscription> ToDomainModel(this IEnumerable<dbSubscription> entities)
        {
            List<Subscription> result = new List<Subscription>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbAccount ToDbModel(this Account entity, dbAccount destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbAccount(); }
                return am.Mapper.Map<Account, dbAccount>(entity, destination);
            }
            return null;
        }
        public static Account ToDomainModel(this dbAccount entity, Account destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Account(); }
                return am.Mapper.Map<dbAccount, Account>(entity, destination);
            }
            return null;
        }
        public static List<Account> ToDomainModel(this IEnumerable<dbAccount> entities)
        {
            List<Account> result = new List<Account>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
        public static dbAsset ToDbModel(this Asset entity, dbAsset destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new dbAsset(); }
                return am.Mapper.Map<Asset, dbAsset>(entity, destination);
            }
            return null;
        }
        public static Asset ToDomainModel(this dbAsset entity, Asset destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Asset(); }
                return am.Mapper.Map<dbAsset, Asset>(entity, destination);
            }
            return null;
        }
        public static List<Asset> ToDomainModel(this IEnumerable<dbAsset> entities)
        {
            List<Asset> result = new List<Asset>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToDomainModel());
                }
            }
            return result;
        }
        
        
        
    }
}

