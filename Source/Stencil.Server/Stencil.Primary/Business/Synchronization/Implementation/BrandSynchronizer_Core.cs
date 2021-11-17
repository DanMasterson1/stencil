using Codeable.Foundation.Common;
using Stencil.Common;
using Stencil.Domain;
using Stencil.Primary.Business.Index;
using Stencil.Primary.Health;
using sdk = Stencil.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Codeable.Foundation.Core;

namespace Stencil.Primary.Synchronization.Implementation
{
    public partial class BrandSynchronizer : SynchronizerBase<Guid>, IBrandSynchronizer
    {
        public BrandSynchronizer(IFoundation foundation)
            : base(foundation, "BrandSynchronizer")
        {

        }

        public override int Priority
        {
            get
            {
                return 15;
            }
        }

        public override void PerformSynchronizationForItem(Guid primaryKey)
        {
            base.ExecuteMethod("PerformSynchronizationForItem", delegate ()
            {
                Brand domainModel = this.API.Direct.Brands.GetById(primaryKey);
                if (domainModel != null)
                {
                    Action<Guid, bool, DateTime, string> synchronizationUpdateMethod = this.API.Direct.Brands.SynchronizationUpdate;
                    if(this.API.Integration.SettingsResolver.IsHydrate())
                    {
                        synchronizationUpdateMethod = this.API.Direct.Brands.SynchronizationHydrateUpdate;
                    }
                    DateTime syncDate = DateTime.UtcNow;
                    if (domainModel.sync_invalid_utc.HasValue)
                    {
                        syncDate = domainModel.sync_invalid_utc.Value;
                    }
                    try
                    {
                        sdk.Brand sdkModel = domainModel.ToSDKModel();
                        
                        this.HydrateSDKModelComputed(domainModel, sdkModel);
                        this.HydrateSDKModel(domainModel, sdkModel);

                        if (domainModel.deleted_utc.HasValue)
                        {
                            this.API.Index.Brands.DeleteDocument(sdkModel);
                            synchronizationUpdateMethod(domainModel.brand_id, true, syncDate, null);
                        }
                        else
                        {
                            IndexResult result = this.API.Index.Brands.UpdateDocument(sdkModel);
                            if (result.success)
                            {
                                synchronizationUpdateMethod(domainModel.brand_id, true, syncDate, result.ToString());
                            }
                            else
                            {
                                throw new Exception(result.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.IFoundation.LogError(ex, "PerformSynchronizationForItem");
                        HealthReporter.Current.UpdateMetric(HealthTrackType.Each, string.Format(HealthReporter.INDEXER_ERROR_SYNC, this.EntityName), 0, 1);
                        synchronizationUpdateMethod(primaryKey, false, syncDate, CoreUtility.FormatException(ex));
                    }
                }
            });
        }

        public override int PerformSynchronization(string requestedAgentName)
        {
            return base.ExecuteFunction("PerformSynchronization", delegate ()
            {
                string agentName = requestedAgentName;
                if(string.IsNullOrEmpty(agentName))
                {
                    agentName = this.AgentName;
                }
                List<Guid?> invalidItems = new List<Guid?>();
                if(this.API.Integration.SettingsResolver.IsHydrate())
                {
                    invalidItems = this.API.Direct.Brands.SynchronizationHydrateGetInvalid(CommonAssumptions.INDEX_RETRY_THRESHOLD_SECONDS, agentName);
                }
                else
                {
                    invalidItems = this.API.Direct.Brands.SynchronizationGetInvalid(CommonAssumptions.INDEX_RETRY_THRESHOLD_SECONDS, agentName);
                }
                foreach (Guid? item in invalidItems)
                {
                    this.PerformSynchronizationForItem(item.GetValueOrDefault());
                }
                return invalidItems.Count;
            });
        }
        
        /// <summary>
        /// Computed and Calculated Aggs, Typically Generated
        /// </summary>
        protected void HydrateSDKModelComputed(Brand domainModel, sdk.Brand sdkModel)
        {
            
            sdkModel.product_count = this.API.Index.Products.GetCount(sdkModel.brand_id);
            
            sdkModel.listing_count = this.API.Index.Listings.GetCount(sdkModel.brand_id);
            
        }
        partial void HydrateSDKModel(Brand domainModel, sdk.Brand sdkModel);
    }
}
