using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Data.Sql
{
    public static class DatabaseExtensions
    {
        
        public static void InvalidateSync(this dbBrand model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbProduct model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbPromotion model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbListing model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbOrder model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbLineItem model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbInvoice model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbPayment model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbShipment model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbPaymentTransaction model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbPaymentDetail model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
        public static void InvalidateSync(this dbAccount model, string agent, string reason)
        {
            if (model != null)
            {
                model.sync_attempt_utc = null;
                model.sync_success_utc = null;
                model.sync_hydrate_utc = null;
                model.sync_log = reason;
                model.sync_invalid_utc = DateTime.UtcNow;
                model.sync_agent = agent;
            }
        }
        
    }
}
