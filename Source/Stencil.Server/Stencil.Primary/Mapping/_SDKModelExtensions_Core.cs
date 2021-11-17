using am = AutoMapper;
using Codeable.Foundation.Core;
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
        
        public static GlobalSetting ToDomainModel(this SDK.Models.GlobalSetting entity, GlobalSetting destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.GlobalSetting(); }
                GlobalSetting result = am.Mapper.Map<SDK.Models.GlobalSetting, GlobalSetting>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.GlobalSetting ToSDKModel(this GlobalSetting entity, SDK.Models.GlobalSetting destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.GlobalSetting(); }
                SDK.Models.GlobalSetting result = am.Mapper.Map<GlobalSetting, SDK.Models.GlobalSetting>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.GlobalSetting> ToSDKModel(this IEnumerable<GlobalSetting> entities)
        {
            List<SDK.Models.GlobalSetting> result = new List<SDK.Models.GlobalSetting>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Brand ToDomainModel(this SDK.Models.Brand entity, Brand destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Brand(); }
                Brand result = am.Mapper.Map<SDK.Models.Brand, Brand>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Brand ToSDKModel(this Brand entity, SDK.Models.Brand destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Brand(); }
                SDK.Models.Brand result = am.Mapper.Map<Brand, SDK.Models.Brand>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Brand> ToSDKModel(this IEnumerable<Brand> entities)
        {
            List<SDK.Models.Brand> result = new List<SDK.Models.Brand>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Product ToDomainModel(this SDK.Models.Product entity, Product destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Product(); }
                Product result = am.Mapper.Map<SDK.Models.Product, Product>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Product ToSDKModel(this Product entity, SDK.Models.Product destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Product(); }
                SDK.Models.Product result = am.Mapper.Map<Product, SDK.Models.Product>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Product> ToSDKModel(this IEnumerable<Product> entities)
        {
            List<SDK.Models.Product> result = new List<SDK.Models.Product>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Promotion ToDomainModel(this SDK.Models.Promotion entity, Promotion destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Promotion(); }
                Promotion result = am.Mapper.Map<SDK.Models.Promotion, Promotion>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Promotion ToSDKModel(this Promotion entity, SDK.Models.Promotion destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Promotion(); }
                SDK.Models.Promotion result = am.Mapper.Map<Promotion, SDK.Models.Promotion>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Promotion> ToSDKModel(this IEnumerable<Promotion> entities)
        {
            List<SDK.Models.Promotion> result = new List<SDK.Models.Promotion>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Listing ToDomainModel(this SDK.Models.Listing entity, Listing destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Listing(); }
                Listing result = am.Mapper.Map<SDK.Models.Listing, Listing>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Listing ToSDKModel(this Listing entity, SDK.Models.Listing destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Listing(); }
                SDK.Models.Listing result = am.Mapper.Map<Listing, SDK.Models.Listing>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Listing> ToSDKModel(this IEnumerable<Listing> entities)
        {
            List<SDK.Models.Listing> result = new List<SDK.Models.Listing>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Order ToDomainModel(this SDK.Models.Order entity, Order destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Order(); }
                Order result = am.Mapper.Map<SDK.Models.Order, Order>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Order ToSDKModel(this Order entity, SDK.Models.Order destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Order(); }
                SDK.Models.Order result = am.Mapper.Map<Order, SDK.Models.Order>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Order> ToSDKModel(this IEnumerable<Order> entities)
        {
            List<SDK.Models.Order> result = new List<SDK.Models.Order>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static LineItem ToDomainModel(this SDK.Models.LineItem entity, LineItem destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.LineItem(); }
                LineItem result = am.Mapper.Map<SDK.Models.LineItem, LineItem>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.LineItem ToSDKModel(this LineItem entity, SDK.Models.LineItem destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.LineItem(); }
                SDK.Models.LineItem result = am.Mapper.Map<LineItem, SDK.Models.LineItem>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.LineItem> ToSDKModel(this IEnumerable<LineItem> entities)
        {
            List<SDK.Models.LineItem> result = new List<SDK.Models.LineItem>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Invoice ToDomainModel(this SDK.Models.Invoice entity, Invoice destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Invoice(); }
                Invoice result = am.Mapper.Map<SDK.Models.Invoice, Invoice>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Invoice ToSDKModel(this Invoice entity, SDK.Models.Invoice destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Invoice(); }
                SDK.Models.Invoice result = am.Mapper.Map<Invoice, SDK.Models.Invoice>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Invoice> ToSDKModel(this IEnumerable<Invoice> entities)
        {
            List<SDK.Models.Invoice> result = new List<SDK.Models.Invoice>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Payment ToDomainModel(this SDK.Models.Payment entity, Payment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Payment(); }
                Payment result = am.Mapper.Map<SDK.Models.Payment, Payment>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Payment ToSDKModel(this Payment entity, SDK.Models.Payment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Payment(); }
                SDK.Models.Payment result = am.Mapper.Map<Payment, SDK.Models.Payment>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Payment> ToSDKModel(this IEnumerable<Payment> entities)
        {
            List<SDK.Models.Payment> result = new List<SDK.Models.Payment>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Shipment ToDomainModel(this SDK.Models.Shipment entity, Shipment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Shipment(); }
                Shipment result = am.Mapper.Map<SDK.Models.Shipment, Shipment>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Shipment ToSDKModel(this Shipment entity, SDK.Models.Shipment destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Shipment(); }
                SDK.Models.Shipment result = am.Mapper.Map<Shipment, SDK.Models.Shipment>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Shipment> ToSDKModel(this IEnumerable<Shipment> entities)
        {
            List<SDK.Models.Shipment> result = new List<SDK.Models.Shipment>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static PaymentTransaction ToDomainModel(this SDK.Models.PaymentTransaction entity, PaymentTransaction destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.PaymentTransaction(); }
                PaymentTransaction result = am.Mapper.Map<SDK.Models.PaymentTransaction, PaymentTransaction>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.PaymentTransaction ToSDKModel(this PaymentTransaction entity, SDK.Models.PaymentTransaction destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.PaymentTransaction(); }
                SDK.Models.PaymentTransaction result = am.Mapper.Map<PaymentTransaction, SDK.Models.PaymentTransaction>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.PaymentTransaction> ToSDKModel(this IEnumerable<PaymentTransaction> entities)
        {
            List<SDK.Models.PaymentTransaction> result = new List<SDK.Models.PaymentTransaction>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static PaymentDetail ToDomainModel(this SDK.Models.PaymentDetail entity, PaymentDetail destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.PaymentDetail(); }
                PaymentDetail result = am.Mapper.Map<SDK.Models.PaymentDetail, PaymentDetail>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.PaymentDetail ToSDKModel(this PaymentDetail entity, SDK.Models.PaymentDetail destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.PaymentDetail(); }
                SDK.Models.PaymentDetail result = am.Mapper.Map<PaymentDetail, SDK.Models.PaymentDetail>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.PaymentDetail> ToSDKModel(this IEnumerable<PaymentDetail> entities)
        {
            List<SDK.Models.PaymentDetail> result = new List<SDK.Models.PaymentDetail>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Subscription ToDomainModel(this SDK.Models.Subscription entity, Subscription destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Subscription(); }
                Subscription result = am.Mapper.Map<SDK.Models.Subscription, Subscription>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Subscription ToSDKModel(this Subscription entity, SDK.Models.Subscription destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Subscription(); }
                SDK.Models.Subscription result = am.Mapper.Map<Subscription, SDK.Models.Subscription>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Subscription> ToSDKModel(this IEnumerable<Subscription> entities)
        {
            List<SDK.Models.Subscription> result = new List<SDK.Models.Subscription>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Account ToDomainModel(this SDK.Models.Account entity, Account destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Account(); }
                Account result = am.Mapper.Map<SDK.Models.Account, Account>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Account ToSDKModel(this Account entity, SDK.Models.Account destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Account(); }
                SDK.Models.Account result = am.Mapper.Map<Account, SDK.Models.Account>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Account> ToSDKModel(this IEnumerable<Account> entities)
        {
            List<SDK.Models.Account> result = new List<SDK.Models.Account>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
        public static Asset ToDomainModel(this SDK.Models.Asset entity, Asset destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new Domain.Asset(); }
                Asset result = am.Mapper.Map<SDK.Models.Asset, Asset>(entity, destination);
                return result;
            }
            return null;
        }
        public static SDK.Models.Asset ToSDKModel(this Asset entity, SDK.Models.Asset destination = null)
        {
            if (entity != null)
            {
                if (destination == null) { destination = new SDK.Models.Asset(); }
                SDK.Models.Asset result = am.Mapper.Map<Asset, SDK.Models.Asset>(entity, destination);
                return result;
            }
            return null;
        }
        public static List<SDK.Models.Asset> ToSDKModel(this IEnumerable<Asset> entities)
        {
            List<SDK.Models.Asset> result = new List<SDK.Models.Asset>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    result.Add(item.ToSDKModel());
                }
            }
            return result;
        }
        
        
        
    }
}

