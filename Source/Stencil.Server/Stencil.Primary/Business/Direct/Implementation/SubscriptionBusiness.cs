using Stencil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Direct.Implementation
{
    partial class SubscriptionBusiness
    {
        public List<Subscription> GetByBrandAndEvent(Guid brand_id, string eventName)
        {
            return base.ExecuteFunction("GetByBrandId", delegate ()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbSubscriptions
                                  where (n.brand_id == brand_id) && (n.event_name == eventName)
                                  select n);
                    return result.ToDomainModel();
                }
            });
        }

        public List<Guid> GetOrderSubscribers()
        {
            return base.ExecuteFunction("GetOrderSubscribers", delegate ()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbSubscriptions
                                  where n.event_name == "ProductOrdered"
                                  select n.brand_id);
                    return result.ToList();
                }
            });
        }
    }
}
