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
    public partial class SubscriptionBusiness : BusinessBase, ISubscriptionBusiness
    {
        public SubscriptionBusiness(IFoundation foundation)
            : base(foundation, "Subscription")
        {
        }
        
        

        public Subscription Insert(Subscription insertSubscription)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertSubscription, true);
                    var interception = this.Intercept(insertSubscription, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertSubscription.subscription_id == Guid.Empty)
                    {
                        insertSubscription.subscription_id = Guid.NewGuid();
                    }
                    

                    dbSubscription dbModel = insertSubscription.ToDbModel();
                    
                    

                    db.dbSubscriptions.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    
                    this.DependencyCoordinator.SubscriptionInvalidated(Dependency.None, dbModel.subscription_id);
                }
                return this.GetById(insertSubscription.subscription_id);
            });
        }
        public Subscription Update(Subscription updateSubscription)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateSubscription, false);
                    var interception = this.Intercept(updateSubscription, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    
                    
                    dbSubscription found = (from n in db.dbSubscriptions
                                    where n.subscription_id == updateSubscription.subscription_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        Subscription previous = found.ToDomainModel();
                        
                        found = updateSubscription.ToDbModel(found);
                        
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        
                        this.DependencyCoordinator.SubscriptionInvalidated(Dependency.None, found.subscription_id);
                    
                    }
                    
                    return this.GetById(updateSubscription.subscription_id);
                }
            });
        }
        public void Delete(Guid subscription_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbSubscription found = (from a in db.dbSubscriptions
                                    where a.subscription_id == subscription_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        db.dbSubscriptions.Remove(found);
                        
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        
                        this.DependencyCoordinator.SubscriptionInvalidated(Dependency.None, found.subscription_id);
                    }
                }
            });
        }
        
        public Subscription GetById(Guid subscription_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbSubscription result = (from n in db.dbSubscriptions
                                     where (n.subscription_id == subscription_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<Subscription> GetByBrandId(Guid brand_id)
        {
            return base.ExecuteFunction("GetByBrandId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbSubscriptions
                                     where (n.brand_id == brand_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        public List<Subscription> GetByProductId(Guid product_id)
        {
            return base.ExecuteFunction("GetByProductId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbSubscriptions
                                     where (n.product_id == product_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        


        
        
        public InterceptArgs<Subscription> Intercept(Subscription subscription, bool forInsert)
        {
            InterceptArgs<Subscription> args = new InterceptArgs<Subscription>()
            {
                ForInsert = forInsert,
                ReturnEntity = subscription
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<Subscription> args);
        partial void PreProcess(Subscription subscription, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbSubscription subscription);
        partial void AfterUpdatePersisted(StencilContext db, dbSubscription subscription, Subscription previous);
        partial void AfterDeletePersisted(StencilContext db, dbSubscription subscription);
        
    }
}

