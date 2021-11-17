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
    // WARNING: THIS FILE IS GENERATED
    public partial class OrderBusiness : BusinessBase, IOrderBusiness
    {
        public OrderBusiness(IFoundation foundation)
            : base(foundation, "Order")
        {
        }
        
        protected IOrderSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IOrderSynchronizer>();
            }
        }

        public Order Insert(Order insertOrder)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertOrder, true);
                    var interception = this.Intercept(insertOrder, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertOrder.order_id == Guid.Empty)
                    {
                        insertOrder.order_id = Guid.NewGuid();
                    }
                    insertOrder.created_utc = DateTime.UtcNow;
                    insertOrder.updated_utc = insertOrder.created_utc;

                    dbOrder dbModel = insertOrder.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbOrders.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.order_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.OrderInvalidated(Dependency.None, dbModel.order_id);
                }
                return this.GetById(insertOrder.order_id);
            });
        }
        public Order Update(Order updateOrder)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateOrder, false);
                    var interception = this.Intercept(updateOrder, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updateOrder.updated_utc = DateTime.UtcNow;
                    
                    dbOrder found = (from n in db.dbOrders
                                    where n.order_id == updateOrder.order_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Order previous = found.ToDomainModel();
                        
                        found = updateOrder.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.order_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.OrderInvalidated(Dependency.None, found.order_id);
                    
                    }
                    
                    return this.GetById(updateOrder.order_id);
                }
            });
        }
        public void Delete(Guid order_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbOrder found = (from a in db.dbOrders
                                    where a.order_id == order_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.order_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.OrderInvalidated(Dependency.None, found.order_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid order_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spOrder_SyncUpdate(order_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spOrder_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid order_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spOrder_HydrateSyncUpdate(order_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spOrder_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Order GetById(Guid order_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbOrder result = (from n in db.dbOrders
                                     where (n.order_id == order_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<Order> GetByAccountId(Guid account_id)
        {
            return base.ExecuteFunction("GetByAccountId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbOrders
                                     where (n.account_id == account_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForAccountId(Guid account_id, string reason)
        {
            base.ExecuteMethod("InvalidateForAccountId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbOrders
                        .Where(x => x.account_id == account_id)
                        .Update(x => new dbOrder() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbLineItems
                        .Where(x => x.Order.account_id == account_id)
                        .Update(x => new dbLineItem()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbInvoices
                        .Where(x => x.Order.account_id == account_id)
                        .Update(x => new dbInvoice()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbPayments
                        .Where(x => x.Order.account_id == account_id)
                        .Update(x => new dbPayment()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbShipments
                        .Where(x => x.Order.account_id == account_id)
                        .Update(x => new dbShipment()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbPaymentTransactions
                        .Where(x => x.Order.account_id == account_id)
                        .Update(x => new dbPaymentTransaction()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        public List<Order> GetByInvoiceId(Guid invoice_id)
        {
            return base.ExecuteFunction("GetByInvoiceId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbOrders
                                     where (n.invoice_id == invoice_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        public List<Order> GetByPaymentId(Guid payment_id)
        {
            return base.ExecuteFunction("GetByPaymentId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbOrders
                                     where (n.payment_id == payment_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForPaymentId(Guid payment_id, string reason)
        {
            base.ExecuteMethod("InvalidateForPaymentId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbOrders
                        .Where(x => x.payment_id == payment_id)
                        .Update(x => new dbOrder() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbLineItems
                        .Where(x => x.Order.payment_id == payment_id)
                        .Update(x => new dbLineItem()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbInvoices
                        .Where(x => x.Order.payment_id == payment_id)
                        .Update(x => new dbInvoice()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbPayments
                        .Where(x => x.Order.payment_id == payment_id)
                        .Update(x => new dbPayment()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbShipments
                        .Where(x => x.Order.payment_id == payment_id)
                        .Update(x => new dbShipment()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbPaymentTransactions
                        .Where(x => x.Order.payment_id == payment_id)
                        .Update(x => new dbPaymentTransaction()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        public List<Order> GetByShipmentId(Guid shipment_id)
        {
            return base.ExecuteFunction("GetByShipmentId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbOrders
                                     where (n.shipment_id == shipment_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForShipmentId(Guid shipment_id, string reason)
        {
            base.ExecuteMethod("InvalidateForShipmentId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbOrders
                        .Where(x => x.shipment_id == shipment_id)
                        .Update(x => new dbOrder() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbLineItems
                        .Where(x => x.Order.shipment_id == shipment_id)
                        .Update(x => new dbLineItem()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbInvoices
                        .Where(x => x.Order.shipment_id == shipment_id)
                        .Update(x => new dbInvoice()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbPayments
                        .Where(x => x.Order.shipment_id == shipment_id)
                        .Update(x => new dbPayment()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbShipments
                        .Where(x => x.Order.shipment_id == shipment_id)
                        .Update(x => new dbShipment()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbPaymentTransactions
                        .Where(x => x.Order.shipment_id == shipment_id)
                        .Update(x => new dbPaymentTransaction()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid order_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbOrders
                        .Where(x => x.order_id == order_id)
                        .Update(x => new dbOrder() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<Order> Intercept(Order order, bool forInsert)
        {
            InterceptArgs<Order> args = new InterceptArgs<Order>()
            {
                ForInsert = forInsert,
                ReturnEntity = order
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Order> args);
        partial void PreProcess(Order order, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbOrder order);
        partial void AfterUpdatePersisted(StencilContext db, dbOrder order, Order previous);
        partial void AfterDeletePersisted(StencilContext db, dbOrder order);
        partial void AfterUpdateIndexed(StencilContext db, dbOrder order);
        partial void AfterInsertIndexed(StencilContext db, dbOrder order);
    }
}

