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
    public partial class BrandBusiness : BusinessBase, IBrandBusiness
    {
        public BrandBusiness(IFoundation foundation)
            : base(foundation, "Brand")
        {
        }
        
        protected IBrandSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IBrandSynchronizer>();
            }
        }

        public Brand Insert(Brand insertBrand)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertBrand, true);
                    var interception = this.Intercept(insertBrand, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertBrand.brand_id == Guid.Empty)
                    {
                        insertBrand.brand_id = Guid.NewGuid();
                    }
                    insertBrand.created_utc = DateTime.UtcNow;
                    insertBrand.updated_utc = insertBrand.created_utc;

                    dbBrand dbModel = insertBrand.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbBrands.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.brand_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.BrandInvalidated(Dependency.None, dbModel.brand_id);
                }
                return this.GetById(insertBrand.brand_id);
            });
        }
        public Brand Update(Brand updateBrand)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateBrand, false);
                    var interception = this.Intercept(updateBrand, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updateBrand.updated_utc = DateTime.UtcNow;
                    
                    dbBrand found = (from n in db.dbBrands
                                    where n.brand_id == updateBrand.brand_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Brand previous = found.ToDomainModel();
                        
                        found = updateBrand.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.brand_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.BrandInvalidated(Dependency.None, found.brand_id);
                    
                    }
                    
                    return this.GetById(updateBrand.brand_id);
                }
            });
        }
        public void Delete(Guid brand_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbBrand found = (from a in db.dbBrands
                                    where a.brand_id == brand_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.brand_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.BrandInvalidated(Dependency.None, found.brand_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid brand_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spBrand_SyncUpdate(brand_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spBrand_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid brand_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spBrand_HydrateSyncUpdate(brand_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spBrand_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Brand GetById(Guid brand_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbBrand result = (from n in db.dbBrands
                                     where (n.brand_id == brand_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        
        public void Invalidate(Guid brand_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbBrands
                        .Where(x => x.brand_id == brand_id)
                        .Update(x => new dbBrand() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        public List<Brand> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("Find", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    if(string.IsNullOrEmpty(keyword))
                    { 
                        keyword = ""; 
                    }

                    var data = (from p in db.dbBrands
                                where (keyword == "" 
                                    || p.brand_name.Contains(keyword)
                                )
                                select p);

                    List<dbBrand> result = new List<dbBrand>();

                    switch (order_by)
                    {
                        case "brand_name":
                            if (!descending)
                            {
                                result = data.OrderBy(s => s.brand_name).Skip(skip).Take(take).ToList();
                            }
                            else
                            {
                                result = data.OrderByDescending(s => s.brand_name).Skip(skip).Take(take).ToList();
                            }
                            break;
                        
                        default:
                            result = data.OrderBy(s => s.brand_id).Skip(skip).Take(take).ToList();
                            break;
                    }
                    return result.ToDomainModel();
                }
            });
        }
        


        
        
        public InterceptArgs<Brand> Intercept(Brand brand, bool forInsert)
        {
            InterceptArgs<Brand> args = new InterceptArgs<Brand>()
            {
                ForInsert = forInsert,
                ReturnEntity = brand
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Brand> args);
        partial void PreProcess(Brand brand, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbBrand brand);
        partial void AfterUpdatePersisted(StencilContext db, dbBrand brand, Brand previous);
        partial void AfterDeletePersisted(StencilContext db, dbBrand brand);
        partial void AfterUpdateIndexed(StencilContext db, dbBrand brand);
        partial void AfterInsertIndexed(StencilContext db, dbBrand brand);
    }
}

