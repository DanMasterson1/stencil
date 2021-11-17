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
    public partial class InvoiceBusiness : BusinessBase, IInvoiceBusiness
    {
        public InvoiceBusiness(IFoundation foundation)
            : base(foundation, "Invoice")
        {
        }
        
        protected IInvoiceSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IInvoiceSynchronizer>();
            }
        }

        public Invoice Insert(Invoice insertInvoice)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertInvoice, true);
                    var interception = this.Intercept(insertInvoice, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertInvoice.invoice_id == Guid.Empty)
                    {
                        insertInvoice.invoice_id = Guid.NewGuid();
                    }
                    insertInvoice.created_utc = DateTime.UtcNow;
                    insertInvoice.updated_utc = insertInvoice.created_utc;

                    dbInvoice dbModel = insertInvoice.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbInvoices.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.invoice_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.InvoiceInvalidated(Dependency.None, dbModel.invoice_id);
                }
                return this.GetById(insertInvoice.invoice_id);
            });
        }
        public Invoice Update(Invoice updateInvoice)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateInvoice, false);
                    var interception = this.Intercept(updateInvoice, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updateInvoice.updated_utc = DateTime.UtcNow;
                    
                    dbInvoice found = (from n in db.dbInvoices
                                    where n.invoice_id == updateInvoice.invoice_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Invoice previous = found.ToDomainModel();
                        
                        found = updateInvoice.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.invoice_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.InvoiceInvalidated(Dependency.None, found.invoice_id);
                    
                    }
                    
                    return this.GetById(updateInvoice.invoice_id);
                }
            });
        }
        public void Delete(Guid invoice_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbInvoice found = (from a in db.dbInvoices
                                    where a.invoice_id == invoice_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.invoice_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.InvoiceInvalidated(Dependency.None, found.invoice_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid invoice_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spInvoice_SyncUpdate(invoice_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spInvoice_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid invoice_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spInvoice_HydrateSyncUpdate(invoice_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spInvoice_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Invoice GetById(Guid invoice_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbInvoice result = (from n in db.dbInvoices
                                     where (n.invoice_id == invoice_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<Invoice> GetByOrderId(Guid order_id)
        {
            return base.ExecuteFunction("GetByOrderId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbInvoices
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
                    db.dbInvoices
                        .Where(x => x.order_id == order_id)
                        .Update(x => new dbInvoice() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        public List<Invoice> GetByAssetId(Guid asset_id)
        {
            return base.ExecuteFunction("GetByAssetId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbInvoices
                                     where (n.asset_id == asset_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        public void InvalidateForAssetId(Guid asset_id, string reason)
        {
            base.ExecuteMethod("InvalidateForAssetId", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbInvoices
                        .Where(x => x.asset_id == asset_id)
                        .Update(x => new dbInvoice() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid invoice_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbInvoices
                        .Where(x => x.invoice_id == invoice_id)
                        .Update(x => new dbInvoice() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<Invoice> Intercept(Invoice invoice, bool forInsert)
        {
            InterceptArgs<Invoice> args = new InterceptArgs<Invoice>()
            {
                ForInsert = forInsert,
                ReturnEntity = invoice
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Invoice> args);
        partial void PreProcess(Invoice invoice, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbInvoice invoice);
        partial void AfterUpdatePersisted(StencilContext db, dbInvoice invoice, Invoice previous);
        partial void AfterDeletePersisted(StencilContext db, dbInvoice invoice);
        partial void AfterUpdateIndexed(StencilContext db, dbInvoice invoice);
        partial void AfterInsertIndexed(StencilContext db, dbInvoice invoice);
    }
}

