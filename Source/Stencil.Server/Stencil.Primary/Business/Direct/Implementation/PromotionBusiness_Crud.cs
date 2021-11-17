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
    public partial class PromotionBusiness : BusinessBase, IPromotionBusiness
    {
        public PromotionBusiness(IFoundation foundation)
            : base(foundation, "Promotion")
        {
        }
        
        protected IPromotionSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IPromotionSynchronizer>();
            }
        }

        public Promotion Insert(Promotion insertPromotion)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertPromotion, true);
                    var interception = this.Intercept(insertPromotion, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertPromotion.promotion_id == Guid.Empty)
                    {
                        insertPromotion.promotion_id = Guid.NewGuid();
                    }
                    insertPromotion.created_utc = DateTime.UtcNow;
                    insertPromotion.updated_utc = insertPromotion.created_utc;

                    dbPromotion dbModel = insertPromotion.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbPromotions.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.promotion_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.PromotionInvalidated(Dependency.None, dbModel.promotion_id);
                }
                return this.GetById(insertPromotion.promotion_id);
            });
        }
        public Promotion Update(Promotion updatePromotion)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updatePromotion, false);
                    var interception = this.Intercept(updatePromotion, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updatePromotion.updated_utc = DateTime.UtcNow;
                    
                    dbPromotion found = (from n in db.dbPromotions
                                    where n.promotion_id == updatePromotion.promotion_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Promotion previous = found.ToDomainModel();
                        
                        found = updatePromotion.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.promotion_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.PromotionInvalidated(Dependency.None, found.promotion_id);
                    
                    }
                    
                    return this.GetById(updatePromotion.promotion_id);
                }
            });
        }
        public void Delete(Guid promotion_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbPromotion found = (from a in db.dbPromotions
                                    where a.promotion_id == promotion_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.promotion_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.PromotionInvalidated(Dependency.None, found.promotion_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid promotion_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPromotion_SyncUpdate(promotion_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPromotion_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid promotion_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spPromotion_HydrateSyncUpdate(promotion_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spPromotion_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Promotion GetById(Guid promotion_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbPromotion result = (from n in db.dbPromotions
                                     where (n.promotion_id == promotion_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        
        public void Invalidate(Guid promotion_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbPromotions
                        .Where(x => x.promotion_id == promotion_id)
                        .Update(x => new dbPromotion() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<Promotion> Intercept(Promotion promotion, bool forInsert)
        {
            InterceptArgs<Promotion> args = new InterceptArgs<Promotion>()
            {
                ForInsert = forInsert,
                ReturnEntity = promotion
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Promotion> args);
        partial void PreProcess(Promotion promotion, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbPromotion promotion);
        partial void AfterUpdatePersisted(StencilContext db, dbPromotion promotion, Promotion previous);
        partial void AfterDeletePersisted(StencilContext db, dbPromotion promotion);
        partial void AfterUpdateIndexed(StencilContext db, dbPromotion promotion);
        partial void AfterInsertIndexed(StencilContext db, dbPromotion promotion);
    }
}

