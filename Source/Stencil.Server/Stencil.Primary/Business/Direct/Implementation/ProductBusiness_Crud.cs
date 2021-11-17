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
    public partial class ProductBusiness : BusinessBase, IProductBusiness
    {
        public ProductBusiness(IFoundation foundation)
            : base(foundation, "Product")
        {
        }
        
        protected IProductSynchronizer Synchronizer
        {
            get
            {
                return this.IFoundation.Resolve<IProductSynchronizer>();
            }
        }

        public Product Insert(Product insertProduct)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertProduct, true);
                    var interception = this.Intercept(insertProduct, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertProduct.product_id == Guid.Empty)
                    {
                        insertProduct.product_id = Guid.NewGuid();
                    }
                    insertProduct.created_utc = DateTime.UtcNow;
                    insertProduct.updated_utc = insertProduct.created_utc;

                    dbProduct dbModel = insertProduct.ToDbModel();
                    
                    dbModel.InvalidateSync(this.DefaultAgent, "insert");

                    db.dbProducts.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    this.Synchronizer.SynchronizeItem(dbModel.product_id, Availability.Searchable);
                    this.AfterInsertIndexed(db, dbModel);
                    
                    this.DependencyCoordinator.ProductInvalidated(Dependency.None, dbModel.product_id);
                }
                return this.GetById(insertProduct.product_id);
            });
        }
        public Product Update(Product updateProduct)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateProduct, false);
                    var interception = this.Intercept(updateProduct, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    updateProduct.updated_utc = DateTime.UtcNow;
                    
                    dbProduct found = (from n in db.dbProducts
                                    where n.product_id == updateProduct.product_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Product previous = found.ToDomainModel();
                        
                        found = updateProduct.ToDbModel(found);
                        found.InvalidateSync(this.DefaultAgent, "updated");
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        this.Synchronizer.SynchronizeItem(found.product_id, Availability.Searchable);
                        this.AfterUpdateIndexed(db, found);
                        
                        this.DependencyCoordinator.ProductInvalidated(Dependency.None, found.product_id);
                    
                    }
                    
                    return this.GetById(updateProduct.product_id);
                }
            });
        }
        public void Delete(Guid product_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbProduct found = (from a in db.dbProducts
                                    where a.product_id == product_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        found.deleted_utc = DateTime.UtcNow;
                        found.InvalidateSync(this.DefaultAgent, "deleted");
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        this.Synchronizer.SynchronizeItem(found.product_id, Availability.Searchable);
                        
                        this.DependencyCoordinator.ProductInvalidated(Dependency.None, found.product_id);
                    }
                }
            });
        }
        public void SynchronizationUpdate(Guid product_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spProduct_SyncUpdate(product_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spProduct_SyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        public void SynchronizationHydrateUpdate(Guid product_id, bool success, DateTime sync_date_utc, string sync_log)
        {
            base.ExecuteMethod("SynchronizationHydrateUpdate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.spProduct_HydrateSyncUpdate(product_id, success, sync_date_utc, sync_log);
                }
            });
        }
        public List<Guid?> SynchronizationHydrateGetInvalid(int retryPriorityThreshold, string sync_agent)
        {
            return base.ExecuteFunction("SynchronizationHydrateGetInvalid", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    return db.spProduct_HydrateSyncGetInvalid(retryPriorityThreshold, sync_agent).ToList();
                }
            });
        }
        
        public Product GetById(Guid product_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbProduct result = (from n in db.dbProducts
                                     where (n.product_id == product_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<Product> GetByBrandId(Guid brand_id)
        {
            return base.ExecuteFunction("GetByBrandId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbProducts
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
                    db.dbProducts
                        .Where(x => x.brand_id == brand_id)
                        .Update(x => new dbProduct() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                     db.dbListings
                        .Where(x => x.Product.brand_id == brand_id)
                        .Update(x => new dbListing()
                        {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                    
                }
            });
        }
        
        public void Invalidate(Guid product_id, string reason)
        {
            base.ExecuteMethod("Invalidate", delegate ()
            {
                using (var db = base.CreateSQLContext())
                {
                    db.dbProducts
                        .Where(x => x.product_id == product_id)
                        .Update(x => new dbProduct() {
                            sync_success_utc = null,
                            sync_hydrate_utc = null,
                            sync_invalid_utc = DateTime.UtcNow,
                            sync_log = reason
                        });
                }
            });
        }
        public List<Product> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("Find", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    if(string.IsNullOrEmpty(keyword))
                    { 
                        keyword = ""; 
                    }

                    var data = (from p in db.dbProducts
                                where (keyword == "" 
                                    || p.product_name.Contains(keyword)
                                
                                    || p.product_description.Contains(keyword)
                                )
                                select p);

                    List<dbProduct> result = new List<dbProduct>();

                    switch (order_by)
                    {
                        case "product_name":
                            if (!descending)
                            {
                                result = data.OrderBy(s => s.product_name).Skip(skip).Take(take).ToList();
                            }
                            else
                            {
                                result = data.OrderByDescending(s => s.product_name).Skip(skip).Take(take).ToList();
                            }
                            break;
                        
                        case "baseprice":
                            if (!descending)
                            {
                                result = data.OrderBy(s => s.baseprice).Skip(skip).Take(take).ToList();
                            }
                            else
                            {
                                result = data.OrderByDescending(s => s.baseprice).Skip(skip).Take(take).ToList();
                            }
                            break;
                        
                        default:
                            result = data.OrderBy(s => s.product_id).Skip(skip).Take(take).ToList();
                            break;
                    }
                    return result.ToDomainModel();
                }
            });
        }
        


        
        
        public InterceptArgs<Product> Intercept(Product product, bool forInsert)
        {
            InterceptArgs<Product> args = new InterceptArgs<Product>()
            {
                ForInsert = forInsert,
                ReturnEntity = product
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Product> args);
        partial void PreProcess(Product product, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbProduct product);
        partial void AfterUpdatePersisted(StencilContext db, dbProduct product, Product previous);
        partial void AfterDeletePersisted(StencilContext db, dbProduct product);
        partial void AfterUpdateIndexed(StencilContext db, dbProduct product);
        partial void AfterInsertIndexed(StencilContext db, dbProduct product);
    }
}

