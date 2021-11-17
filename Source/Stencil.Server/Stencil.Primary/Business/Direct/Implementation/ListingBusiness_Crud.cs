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
    public partial class ListingBusiness : BusinessBase, IListingBusiness
    {
        public ListingBusiness(IFoundation foundation)
            : base(foundation, "Listing")
        {
        }
        
        protected IListingSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IListingSynchronizer>();
            }
        }

        public Listing Insert(Listing insertListing)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertListing, true);
                    var interception = this.Intercept(insertListing, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertListing.listing_id == Guid.Empty)
                    {
                        insertListing.listing_id = Guid.NewGuid();
                    }
                    insertListing.created_utc = DateTime.UtcNow;
                    insertListing.updated_utc = insertListing.created_utc;

                    dbListing dbModel = insertListing.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbListings.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.listing_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.ListingInvalidated(Dependency.None, dbModel.listing_id);
                }
                return this.GetById(insertListing.listing_id);
            });
        }
        public Listing Update(Listing updateListing)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateListing, false);
                    var interception = this.Intercept(updateListing, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updateListing.updated_utc = DateTime.UtcNow;
                    
                    dbListing found = (from n in db.dbListings
                                    where n.listing_id == updateListing.listing_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Listing previous = found.ToDomainModel();
                        
                        found = updateListing.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.listing_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.ListingInvalidated(Dependency.None, found.listing_id);
                    
                    }
                    
                    return this.GetById(updateListing.listing_id);
                }
            });
        }
        public void Delete(Guid listing_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbListing found = (from a in db.dbListings
                                    where a.listing_id == listing_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.listing_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.ListingInvalidated(Dependency.None, found.listing_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid listing_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spListing_SyncUpdate(listing_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spListing_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid listing_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spListing_HydrateSyncUpdate(listing_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spListing_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Listing GetById(Guid listing_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbListing result = (from n in db.dbListings
                                     where (n.listing_id == listing_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<Listing> GetByBrandId(Guid brand_id)
        {
            return base.ExecuteFunction("GetByBrandId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbListings
                                     where (n.brand_id == brand_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForBrandId(Guid brand_id, string reason)
        {
            base.ExecuteMethod("InvalidateForBrandId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbListings
                        .Where(x => x.brand_id == brand_id)
                        .Update(x => new dbListing() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbLineItems
                        .Where(x => x.Listing.brand_id == brand_id)
                        .Update(x => new dbLineItem()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        public List<Listing> GetByProductId(Guid product_id)
        {
            return base.ExecuteFunction("GetByProductId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbListings
                                     where (n.product_id == product_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForProductId(Guid product_id, string reason)
        {
            base.ExecuteMethod("InvalidateForProductId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbListings
                        .Where(x => x.product_id == product_id)
                        .Update(x => new dbListing() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbLineItems
                        .Where(x => x.Listing.product_id == product_id)
                        .Update(x => new dbLineItem()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        public List<Listing> GetByPromotionId(Guid promotion_id)
        {
            return base.ExecuteFunction("GetByPromotionId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbListings
                                     where (n.promotion_id == promotion_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForPromotionId(Guid promotion_id, string reason)
        {
            base.ExecuteMethod("InvalidateForPromotionId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbListings
                        .Where(x => x.promotion_id == promotion_id)
                        .Update(x => new dbListing() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbLineItems
                        .Where(x => x.Listing.promotion_id == promotion_id)
                        .Update(x => new dbLineItem()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid listing_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbListings
                        .Where(x => x.listing_id == listing_id)
                        .Update(x => new dbListing() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        public List<Listing> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("Find", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    if(string.IsNullOrEmpty(keyword))
                    { 
                        keyword = ""; 
                    }

                    var data = (from p in db.dbListings
                                where (keyword == "" 
                                    || p.listing_description.Contains(keyword)
                                )
                                select p);

                    List<dbListing> result = new List<dbListing>();

                    switch (order_by)
                    {
                        default:
                            result = data.OrderBy(s => s.listing_id).Skip(skip).Take(take).ToList();
                            break;
                    }
                    return result.ToDomainModel();
                }
            });
        }
        


        
        
        public InterceptArgs<Listing> Intercept(Listing listing, bool forInsert)
        {
            InterceptArgs<Listing> args = new InterceptArgs<Listing>()
            {
                ForInsert = forInsert,
                ReturnEntity = listing
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Listing> args);
        partial void PreProcess(Listing listing, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbListing listing);
        partial void AfterUpdatePersisted(StencilContext db, dbListing listing, Listing previous);
        partial void AfterDeletePersisted(StencilContext db, dbListing listing);
        partial void AfterUpdateIndexed(StencilContext db, dbListing listing);
        partial void AfterInsertIndexed(StencilContext db, dbListing listing);
    }
}

