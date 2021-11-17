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
    public partial class PaymentTransactionBusiness : BusinessBase, IPaymentTransactionBusiness
    {
        public PaymentTransactionBusiness(IFoundation foundation)
            : base(foundation, "PaymentTransaction")
        {
        }
        
        protected IPaymentTransactionSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IPaymentTransactionSynchronizer>();
            }
        }

        public PaymentTransaction Insert(PaymentTransaction insertPaymentTransaction)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertPaymentTransaction, true);
                    var interception = this.Intercept(insertPaymentTransaction, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertPaymentTransaction.paymenttransaction_id == Guid.Empty)
                    {
                        insertPaymentTransaction.paymenttransaction_id = Guid.NewGuid();
                    }
                    insertPaymentTransaction.created_utc = DateTime.UtcNow;
                    insertPaymentTransaction.updated_utc = insertPaymentTransaction.created_utc;

                    dbPaymentTransaction dbModel = insertPaymentTransaction.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbPaymentTransactions.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.paymenttransaction_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.PaymentTransactionInvalidated(Dependency.None, dbModel.paymenttransaction_id);
                }
                return this.GetById(insertPaymentTransaction.paymenttransaction_id);
            });
        }
        public PaymentTransaction Update(PaymentTransaction updatePaymentTransaction)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updatePaymentTransaction, false);
                    var interception = this.Intercept(updatePaymentTransaction, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updatePaymentTransaction.updated_utc = DateTime.UtcNow;
                    
                    dbPaymentTransaction found = (from n in db.dbPaymentTransactions
                                    where n.paymenttransaction_id == updatePaymentTransaction.paymenttransaction_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        PaymentTransaction previous = found.ToDomainModel();
                        
                        found = updatePaymentTransaction.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.paymenttransaction_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.PaymentTransactionInvalidated(Dependency.None, found.paymenttransaction_id);
                    
                    }
                    
                    return this.GetById(updatePaymentTransaction.paymenttransaction_id);
                }
            });
        }
        public void Delete(Guid paymenttransaction_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbPaymentTransaction found = (from a in db.dbPaymentTransactions
                                    where a.paymenttransaction_id == paymenttransaction_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.paymenttransaction_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.PaymentTransactionInvalidated(Dependency.None, found.paymenttransaction_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid paymenttransaction_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPaymentTransaction_SyncUpdate(paymenttransaction_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPaymentTransaction_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid paymenttransaction_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPaymentTransaction_HydrateSyncUpdate(paymenttransaction_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPaymentTransaction_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public PaymentTransaction GetById(Guid paymenttransaction_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbPaymentTransaction result = (from n in db.dbPaymentTransactions
                                     where (n.paymenttransaction_id == paymenttransaction_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<PaymentTransaction> GetByOrderId(Guid order_id)
        {
            return base.ExecuteFunction("GetByOrderId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbPaymentTransactions
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
                    db.dbPaymentTransactions
                        .Where(x => x.order_id == order_id)
                        .Update(x => new dbPaymentTransaction() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        public List<PaymentTransaction> GetByPaymentId(Guid payment_id)
        {
            return base.ExecuteFunction("GetByPaymentId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbPaymentTransactions
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
                    db.dbPaymentTransactions
                        .Where(x => x.payment_id == payment_id)
                        .Update(x => new dbPaymentTransaction() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid paymenttransaction_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbPaymentTransactions
                        .Where(x => x.paymenttransaction_id == paymenttransaction_id)
                        .Update(x => new dbPaymentTransaction() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<PaymentTransaction> Intercept(PaymentTransaction paymenttransaction, bool forInsert)
        {
            InterceptArgs<PaymentTransaction> args = new InterceptArgs<PaymentTransaction>()
            {
                ForInsert = forInsert,
                ReturnEntity = paymenttransaction
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<PaymentTransaction> args);
        partial void PreProcess(PaymentTransaction paymenttransaction, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbPaymentTransaction paymenttransaction);
        partial void AfterUpdatePersisted(StencilContext db, dbPaymentTransaction paymenttransaction, PaymentTransaction previous);
        partial void AfterDeletePersisted(StencilContext db, dbPaymentTransaction paymenttransaction);
        partial void AfterUpdateIndexed(StencilContext db, dbPaymentTransaction paymenttransaction);
        partial void AfterInsertIndexed(StencilContext db, dbPaymentTransaction paymenttransaction);
    }
}

