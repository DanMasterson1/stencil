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
    public partial class EntitySubscriptionBusiness : BusinessBase, IEntitySubscriptionBusiness
    {
        public EntitySubscriptionBusiness(IFoundation foundation)
            : base(foundation, "EntitySubscription")
        {
        }
        
        

        public EntitySubscription Insert(EntitySubscription insertEntitySubscription)
        {
            return base.ExecuteFunction("Insert", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    

                    this.PreProcess(insertEntitySubscription, true);
                    var interception = this.Intercept(insertEntitySubscription, true);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    if (insertEntitySubscription.subscription_id == Guid.Empty)
                    {
                        insertEntitySubscription.subscription_id = Guid.NewGuid();
                    }
                    

                    dbEntitySubscription dbModel = insertEntitySubscription.ToDbModel();
                    
                    

                    db.dbEntitySubscriptions.Add(dbModel);

                    db.SaveChanges();
                    
                    this.AfterInsertPersisted(db, dbModel);
                    
                    
                    this.DependencyCoordinator.EntitySubscriptionInvalidated(Dependency.None, dbModel.subscription_id);
                }
                return this.GetById(insertEntitySubscription.subscription_id);
            });
        }
        public EntitySubscription Update(EntitySubscription updateEntitySubscription)
        {
            return base.ExecuteFunction("Update", delegate()
            {
                using (var db = base.CreateSQLContext())
                {
                    this.PreProcess(updateEntitySubscription, false);
                    var interception = this.Intercept(updateEntitySubscription, false);
                    if(interception.Intercepted)
                    {
                        return interception.ReturnEntity;
                    }
                    
                    
                    
                    dbEntitySubscription found = (from n in db.dbEntitySubscriptions
                                    where n.subscription_id == updateEntitySubscription.subscription_id
                                    select n).FirstOrDefault();

                    if (found != null)
                    {
                        EntitySubscription previous = found.ToDomainModel();
                        
                        found = updateEntitySubscription.ToDbModel(found);
                        
                        db.SaveChanges();
                        
                        this.AfterUpdatePersisted(db, found, previous);
                        
                        
                        this.DependencyCoordinator.EntitySubscriptionInvalidated(Dependency.None, found.subscription_id);
                    
                    }
                    
                    return this.GetById(updateEntitySubscription.subscription_id);
                }
            });
        }
        public void Delete(Guid subscription_id)
        {
            base.ExecuteMethod("Delete", delegate()
            {
                
                using (var db = base.CreateSQLContext())
                {
                    dbEntitySubscription found = (from a in db.dbEntitySubscriptions
                                    where a.subscription_id == subscription_id
                                    select a).FirstOrDefault();

                    if (found != null)
                    {
                        
                        db.dbEntitySubscriptions.Remove(found);
                        
                        db.SaveChanges();
                        
                        this.AfterDeletePersisted(db, found);
                        
                        
                        this.DependencyCoordinator.EntitySubscriptionInvalidated(Dependency.None, found.subscription_id);
                    }
                }
            });
        }
        
        public EntitySubscription GetById(Guid subscription_id)
        {
            return base.ExecuteFunction("GetById", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    dbEntitySubscription result = (from n in db.dbEntitySubscriptions
                                     where (n.subscription_id == subscription_id)
                                     select n).FirstOrDefault();
                    return result.ToDomainModel();
                }
            });
        }
        public List<EntitySubscription> GetByBrandId(Guid brand_id)
        {
            return base.ExecuteFunction("GetByBrandId", delegate()
            {
                using (var db = this.CreateSQLContext())
                {
                    var result = (from n in db.dbEntitySubscriptions
                                     where (n.brand_id == brand_id)
                                     select n);
                    return result.ToDomainModel();
                }
            });
        }
        
        
        


        
        
        public InterceptArgs<EntitySubscription> Intercept(EntitySubscription entitysubscription, bool forInsert)
        {
            InterceptArgs<EntitySubscription> args = new InterceptArgs<EntitySubscription>()
            {
                ForInsert = forInsert,
                ReturnEntity = entitysubscription
            };
            this.PerformIntercept(args);
            return args;
        }

        partial void PerformIntercept(InterceptArgs<EntitySubscription> args);
        partial void PreProcess(EntitySubscription entitysubscription, bool forInsert);
        partial void AfterInsertPersisted(StencilContext db, dbEntitySubscription entitysubscription);
        partial void AfterUpdatePersisted(StencilContext db, dbEntitySubscription entitysubscription, EntitySubscription previous);
        partial void AfterDeletePersisted(StencilContext db, dbEntitySubscription entitysubscription);
        
    }
}

