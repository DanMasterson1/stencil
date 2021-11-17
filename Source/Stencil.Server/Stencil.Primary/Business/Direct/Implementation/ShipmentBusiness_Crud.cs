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
    public partial class ShipmentBusiness : BusinessBase, IShipmentBusiness
    {
        public ShipmentBusiness(IFoundation foundation)
            : base(foundation, "Shipment")
        {
        }
        
        protected IShipmentSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IShipmentSynchronizer>();
            }
        }

        public Shipment Insert(Shipment insertShipment)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertShipment, true);
                    var interception = this.Intercept(insertShipment, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertShipment.shipment_id == Guid.Empty)
                    {
                        insertShipment.shipment_id = Guid.NewGuid();
                    }
                    insertShipment.created_utc = DateTime.UtcNow;
                    insertShipment.updated_utc = insertShipment.created_utc;

                    dbShipment dbModel = insertShipment.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbShipments.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.shipment_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.ShipmentInvalidated(Dependency.None, dbModel.shipment_id);
                }
                return this.GetById(insertShipment.shipment_id);
            });
        }
        public Shipment Update(Shipment updateShipment)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateShipment, false);
                    var interception = this.Intercept(updateShipment, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updateShipment.updated_utc = DateTime.UtcNow;
                    
                    dbShipment found = (from n in db.dbShipments
                                    where n.shipment_id == updateShipment.shipment_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Shipment previous = found.ToDomainModel();
                        
                        found = updateShipment.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.shipment_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.ShipmentInvalidated(Dependency.None, found.shipment_id);
                    
                    }
                    
                    return this.GetById(updateShipment.shipment_id);
                }
            });
        }
        public void Delete(Guid shipment_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbShipment found = (from a in db.dbShipments
                                    where a.shipment_id == shipment_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.shipment_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.ShipmentInvalidated(Dependency.None, found.shipment_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid shipment_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spShipment_SyncUpdate(shipment_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spShipment_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid shipment_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spShipment_HydrateSyncUpdate(shipment_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spShipment_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Shipment GetById(Guid shipment_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbShipment result = (from n in db.dbShipments
                                     where (n.shipment_id == shipment_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<Shipment> GetByOrderId(Guid order_id)
        {
            return base.ExecuteFunction("GetByOrderId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbShipments
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
                    db.dbShipments
                        .Where(x => x.order_id == order_id)
                        .Update(x => new dbShipment() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbOrders
                        .Where(x => x.Shipment.order_id == order_id)
                        .Update(x => new dbOrder()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid shipment_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbShipments
                        .Where(x => x.shipment_id == shipment_id)
                        .Update(x => new dbShipment() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        
        


        
        
        public InterceptArgs<Shipment> Intercept(Shipment shipment, bool forInsert)
        {
            InterceptArgs<Shipment> args = new InterceptArgs<Shipment>()
            {
                ForInsert = forInsert,
                ReturnEntity = shipment
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Shipment> args);
        partial void PreProcess(Shipment shipment, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbShipment shipment);
        partial void AfterUpdatePersisted(StencilContext db, dbShipment shipment, Shipment previous);
        partial void AfterDeletePersisted(StencilContext db, dbShipment shipment);
        partial void AfterUpdateIndexed(StencilContext db, dbShipment shipment);
        partial void AfterInsertIndexed(StencilContext db, dbShipment shipment);
    }
}

