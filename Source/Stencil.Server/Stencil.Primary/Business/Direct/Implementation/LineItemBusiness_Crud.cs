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
    public partial class LineItemBusiness : BusinessBase, ILineItemBusiness
    {
        public LineItemBusiness(IFoundation foundation)
            : base(foundation, "LineItem")
        {
        }
        
        protected ILineItemSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<ILineItemSynchronizer>();
            }
        }

        public LineItem Insert(LineItem insertLineItem)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertLineItem, true);
                    var interception = this.Intercept(insertLineItem, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertLineItem.lineitem_id == Guid.Empty)
                    {
                        insertLineItem.lineitem_id = Guid.NewGuid();
                    }
                    insertLineItem.created_utc = DateTime.UtcNow;
                    insertLineItem.updated_utc = insertLineItem.created_utc;

                    dbLineItem dbModel = insertLineItem.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbLineItems.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.lineitem_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.LineItemInvalidated(Dependency.None, dbModel.lineitem_id);
                }
                return this.GetById(insertLineItem.lineitem_id);
            });
        }
        public LineItem Update(LineItem updateLineItem)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateLineItem, false);
                    var interception = this.Intercept(updateLineItem, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updateLineItem.updated_utc = DateTime.UtcNow;
                    
                    dbLineItem found = (from n in db.dbLineItems
                                    where n.lineitem_id == updateLineItem.lineitem_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        LineItem previous = found.ToDomainModel();
                        
                        found = updateLineItem.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.lineitem_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.LineItemInvalidated(Dependency.None, found.lineitem_id);
                    
                    }
                    
                    return this.GetById(updateLineItem.lineitem_id);
                }
            });
        }
        public void Delete(Guid lineitem_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbLineItem found = (from a in db.dbLineItems
                                    where a.lineitem_id == lineitem_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.lineitem_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.LineItemInvalidated(Dependency.None, found.lineitem_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid lineitem_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spLineItem_SyncUpdate(lineitem_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spLineItem_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid lineitem_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spLineItem_HydrateSyncUpdate(lineitem_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spLineItem_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public LineItem GetById(Guid lineitem_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbLineItem result = (from n in db.dbLineItems
                                     where (n.lineitem_id == lineitem_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<LineItem> GetByOrderId(Guid order_id)
        {
            return base.ExecuteFunction("GetByOrderId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbLineItems
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
                    db.dbLineItems
                        .Where(x => x.order_id == order_id)
                        .Update(x => new dbLineItem() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        public List<LineItem> GetByListingId(Guid listing_id)
        {
            return base.ExecuteFunction("GetByListingId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbLineItems
                                     where (n.listing_id == listing_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForListingId(Guid listing_id, string reason)
        {
            base.ExecuteMethod("InvalidateForListingId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbLineItems
                        .Where(x => x.listing_id == listing_id)
                        .Update(x => new dbLineItem() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid lineitem_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbLineItems
                        .Where(x => x.lineitem_id == lineitem_id)
                        .Update(x => new dbLineItem() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<LineItem> Intercept(LineItem lineitem, bool forInsert)
        {
            InterceptArgs<LineItem> args = new InterceptArgs<LineItem>()
            {
                ForInsert = forInsert,
                ReturnEntity = lineitem
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<LineItem> args);
        partial void PreProcess(LineItem lineitem, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbLineItem lineitem);
        partial void AfterUpdatePersisted(StencilContext db, dbLineItem lineitem, LineItem previous);
        partial void AfterDeletePersisted(StencilContext db, dbLineItem lineitem);
        partial void AfterUpdateIndexed(StencilContext db, dbLineItem lineitem);
        partial void AfterInsertIndexed(StencilContext db, dbLineItem lineitem);
    }
}

