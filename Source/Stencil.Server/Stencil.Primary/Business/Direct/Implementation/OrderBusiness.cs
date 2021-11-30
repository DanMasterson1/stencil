using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Domain;
using Stencil.Data.Sql;
using Stencil.Primary.Synchronization;

namespace Stencil.Primary.Business.Direct.Implementation
{
    public partial class OrderBusiness
    {
        public Order CloseOrder(Guid order_id)
        {
            return base.ExecuteFunction(nameof(CloseOrder), delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    Order closeOrder = this.GetById(order_id);

                    this.PreProcess(closeOrder, false);
                    var interception = this.Intercept(closeOrder, false);
                    if (interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    closeOrder.updated_utc = DateTime.UtcNow;
                    closeOrder.order_status = OrderStatus.Closed;
                    
                    dbOrder found = (from n in db.dbOrders
                                     where n.order_id == closeOrder.order_id
                                     select n).FirstOrDefault();

                    if (found != null)
                    {
                        Order previous = found.ToDomainModel();

                        found = closeOrder.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();

                        this.AfterUpdatePersisted(db, found, previous);

                        this.Synchronizer.SynchronizeItem(found.order_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);

                        this.DependencyCoordinator.OrderInvalidated(Dependency.None, found.order_id);

                    }

                    return this.GetById(closeOrder.order_id);
                }
            });
        }

   
        public bool IsCloseable(Guid order_id) //TODO: Should this just call GetCloseableOrders and see if it contains the id? Or do the same logic then use contains?
        {
            return base.ExecuteFunction(nameof(IsCloseable), delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    //List<Guid?> closeableOrders = db.spOrder_GetCloseableOrders().ToList();

                    //return closeableOrders.Contains(order_id);

                    var result = (from n in db.dbOrders
                                  where (n.order_id == order_id && n.order_status == 2 
                                  && n.invoice_id != null && n.order_shipped == true 
                                  && n.order_paid == true)
                                  select n);

                    bool closeable = (result.Count() > 0) ? true : false;

                    return closeable;
                }
            });
        }

        public List<Guid> GetNonInvoicedOrders()
        {
            return base.ExecuteFunction(nameof(GetNonInvoicedOrders), delegate ()
            {
                using (var db = base.CreateSQLContext())
                {

                    var result = (from n in db.dbOrders
                                  where (n.order_status == 2 && n.invoice_id == null 
                                  && n.order_shipped == true && n.order_paid == true)
                                  select n);
                    var resultIds = result.Select(x => x.order_id).ToList();

                    return resultIds;
                }
            });
        }

        public List<Guid> GetCloseableOrders()
        {
            return base.ExecuteFunction(nameof(GetCloseableOrders), delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    var result = (from n in db.dbOrders
                                  where (n.order_status == 2 && n.invoice_id != null
                                  && n.order_shipped == true && n.order_paid == true)
                                  select n);

                    var resultIds = result.Select(x => x.order_id).ToList();

                    return resultIds;
                }
            });
        }

    }
}
