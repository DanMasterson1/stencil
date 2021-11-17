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
    public partial class PaymentDetailBusiness : BusinessBase, IPaymentDetailBusiness
    {
        public PaymentDetailBusiness(IFoundation foundation)
            : base(foundation, "PaymentDetail")
        {
        }
        
        protected IPaymentDetailSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IPaymentDetailSynchronizer>();
            }
        }

        public PaymentDetail Insert(PaymentDetail insertPaymentDetail)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertPaymentDetail, true);
                    var interception = this.Intercept(insertPaymentDetail, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertPaymentDetail.paymentdetail_id == Guid.Empty)
                    {
                        insertPaymentDetail.paymentdetail_id = Guid.NewGuid();
                    }
                    insertPaymentDetail.created_utc = DateTime.UtcNow;
                    insertPaymentDetail.updated_utc = insertPaymentDetail.created_utc;

                    dbPaymentDetail dbModel = insertPaymentDetail.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbPaymentDetails.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.paymentdetail_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.PaymentDetailInvalidated(Dependency.None, dbModel.paymentdetail_id);
                }
                return this.GetById(insertPaymentDetail.paymentdetail_id);
            });
        }
        public PaymentDetail Update(PaymentDetail updatePaymentDetail)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updatePaymentDetail, false);
                    var interception = this.Intercept(updatePaymentDetail, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updatePaymentDetail.updated_utc = DateTime.UtcNow;
                    
                    dbPaymentDetail found = (from n in db.dbPaymentDetails
                                    where n.paymentdetail_id == updatePaymentDetail.paymentdetail_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        PaymentDetail previous = found.ToDomainModel();
                        
                        found = updatePaymentDetail.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.paymentdetail_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.PaymentDetailInvalidated(Dependency.None, found.paymentdetail_id);
                    
                    }
                    
                    return this.GetById(updatePaymentDetail.paymentdetail_id);
                }
            });
        }
        public void Delete(Guid paymentdetail_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbPaymentDetail found = (from a in db.dbPaymentDetails
                                    where a.paymentdetail_id == paymentdetail_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.paymentdetail_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.PaymentDetailInvalidated(Dependency.None, found.paymentdetail_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid paymentdetail_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPaymentDetail_SyncUpdate(paymentdetail_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPaymentDetail_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid paymentdetail_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPaymentDetail_HydrateSyncUpdate(paymentdetail_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPaymentDetail_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public PaymentDetail GetById(Guid paymentdetail_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbPaymentDetail result = (from n in db.dbPaymentDetails
                                     where (n.paymentdetail_id == paymentdetail_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<PaymentDetail> GetByAccountId(Guid account_id)
        {
            return base.ExecuteFunction("GetByAccountId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbPaymentDetails
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
                    db.dbPaymentDetails
                        .Where(x => x.account_id == account_id)
                        .Update(x => new dbPaymentDetail() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid paymentdetail_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbPaymentDetails
                        .Where(x => x.paymentdetail_id == paymentdetail_id)
                        .Update(x => new dbPaymentDetail() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<PaymentDetail> Intercept(PaymentDetail paymentdetail, bool forInsert)
        {
            InterceptArgs<PaymentDetail> args = new InterceptArgs<PaymentDetail>()
            {
                ForInsert = forInsert,
                ReturnEntity = paymentdetail
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<PaymentDetail> args);
        partial void PreProcess(PaymentDetail paymentdetail, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbPaymentDetail paymentdetail);
        partial void AfterUpdatePersisted(StencilContext db, dbPaymentDetail paymentdetail, PaymentDetail previous);
        partial void AfterDeletePersisted(StencilContext db, dbPaymentDetail paymentdetail);
        partial void AfterUpdateIndexed(StencilContext db, dbPaymentDetail paymentdetail);
        partial void AfterInsertIndexed(StencilContext db, dbPaymentDetail paymentdetail);
    }
}

