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
    public partial class PaymentBusiness : BusinessBase, IPaymentBusiness
    {
        public PaymentBusiness(IFoundation foundation)
            : base(foundation, "Payment")
        {
        }
        
        protected IPaymentSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IPaymentSynchronizer>();
            }
        }

        public Payment Insert(Payment insertPayment)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertPayment, true);
                    var interception = this.Intercept(insertPayment, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertPayment.payment_id == Guid.Empty)
                    {
                        insertPayment.payment_id = Guid.NewGuid();
                    }
                    insertPayment.created_utc = DateTime.UtcNow;
                    insertPayment.updated_utc = insertPayment.created_utc;

                    dbPayment dbModel = insertPayment.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbPayments.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.payment_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.PaymentInvalidated(Dependency.None, dbModel.payment_id);
                }
                return this.GetById(insertPayment.payment_id);
            });
        }
        public Payment Update(Payment updatePayment)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updatePayment, false);
                    var interception = this.Intercept(updatePayment, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updatePayment.updated_utc = DateTime.UtcNow;
                    
                    dbPayment found = (from n in db.dbPayments
                                    where n.payment_id == updatePayment.payment_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Payment previous = found.ToDomainModel();
                        
                        found = updatePayment.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.payment_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.PaymentInvalidated(Dependency.None, found.payment_id);
                    
                    }
                    
                    return this.GetById(updatePayment.payment_id);
                }
            });
        }
        public void Delete(Guid payment_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbPayment found = (from a in db.dbPayments
                                    where a.payment_id == payment_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.payment_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.PaymentInvalidated(Dependency.None, found.payment_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid payment_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPayment_SyncUpdate(payment_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPayment_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid payment_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPayment_HydrateSyncUpdate(payment_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPayment_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Payment GetById(Guid payment_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbPayment result = (from n in db.dbPayments
                                     where (n.payment_id == payment_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<Payment> GetByOrderId(Guid order_id)
        {
            return base.ExecuteFunction("GetByOrderId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbPayments
                                     where (n.order_id == order_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForOrderId(Guid order_id, string reason)
        {
            base.ExecuteMethod("InvalidateForOrderId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbPayments
                        .Where(x => x.order_id == order_id)
                        .Update(x => new dbPayment() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbOrders
                        .Where(x => x.Payment.order_id == order_id)
                        .Update(x => new dbOrder()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbPaymentTransactions
                        .Where(x => x.Payment.order_id == order_id)
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
        
        public void Invalidate(Guid payment_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbPayments
                        .Where(x => x.payment_id == payment_id)
                        .Update(x => new dbPayment() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<Payment> Intercept(Payment payment, bool forInsert)
        {
            InterceptArgs<Payment> args = new InterceptArgs<Payment>()
            {
                ForInsert = forInsert,
                ReturnEntity = payment
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Payment> args);
        partial void PreProcess(Payment payment, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbPayment payment);
        partial void AfterUpdatePersisted(StencilContext db, dbPayment payment, Payment previous);
        partial void AfterDeletePersisted(StencilContext db, dbPayment payment);
        partial void AfterUpdateIndexed(StencilContext db, dbPayment payment);
        partial void AfterInsertIndexed(StencilContext db, dbPayment payment);
    }
}

