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
    public partial class ListingSynchronizer : SynchronizerBase<Guid>, IListingSynchronizer
    {
        public ListingSynchronizer(IFoundation foundation)
            : base(foundation, "ListingSynchronizer")
        {

        }

        public override int Priority
        {
            get
            {
                return 30;
            }
        }

        public override void PerformSynchronizationForItem(Guid primaryKey)
        {
            base.ExecuteMethod("PerformSynchronizationForItem", delegate ()
            {
                Listing domainModel = this.API.Direct.Listings.GetById(primaryKey);
                if (domainModel != null)
                {
                    Action<Guid, bool, DateTime, string> synchronizationUpdateMethod = this.API.Direct.Listings.SynchronizationUpdate;
                    if(this.API.Integration.SettingsResolver.IsHydrate())
                    {
                        synchronizationUpdateMethod = this.API.Direct.Listings.SynchronizationHydrateUpdate;
                    }
                    DateTime syncDate = DateTime.UtcNow;
                    if (domainModel.sync_invalid_utc.HasValue)
                    {
                        syncDate = domainModel.sync_invalid_utc.Value;
                    }
                    try
                    {
                        sdk.Listing sdkModel = domainModel.ToSDKModel();
                        
                        this.HydrateSDKModelComputed(domainModel, sdkModel);
                        this.HydrateSDKModel(domainModel, sdkModel);

                        if (domainModel.deleted_utc.HasValue)
                        {
                            this.API.Index.Listings.DeleteDocument(sdkModel);
                            synchronizationUpdateMethod(domainModel.listing_id, true, syncDate, null);
                        }
                        else
                        {
                            IndexResult result = this.API.Index.Listings.UpdateDocument(sdkModel);
                            if (result.success)
                            {
                                synchronizationUpdateMethod(domainModel.listing_id, true, syncDate, result.ToString());
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
                    invalidItems = this.API.Direct.Listings.SynchronizationHydrateGetInvalid(CommonAssumptions.INDEX_RETRY_THRESHOLD_SECONDS, agentName);
                }
                else
                {
                    invalidItems = this.API.Direct.Listings.SynchronizationGetInvalid(CommonAssumptions.INDEX_RETRY_THRESHOLD_SECONDS, agentName);
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
        protected void HydrateSDKModelComputed(Listing domainModel, sdk.Listing sdkModel)
        {
            
			sdk.Brand referenceBrand = this.API.Index.Brands.GetById(sdkModel.brand_id);
			if(referenceBrand != null)
			{
				sdkModel.brand_name = referenceBrand.brand_name;
			}
			else
			{
				Brand referenceDomainBrand = this.API.Direct.Brands.GetById(sdkModel.brand_id);
				if(referenceDomainBrand != null)
				{
					sdkModel.brand_name = referenceDomainBrand.brand_name;
				}
			}		
            
			sdk.Product referenceProduct = this.API.Index.Products.GetById(sdkModel.product_id);
			if(referenceProduct != null)
			{
				sdkModel.product_name = referenceProduct.product_name;
			}
			else
			{
				Product referenceDomainProduct = this.API.Direct.Products.GetById(sdkModel.product_id);
				if(referenceDomainProduct != null)
				{
					sdkModel.product_name = referenceDomainProduct.product_name;
				}
			}		

            if(sdkModel.promotion_id != null)
            {
                sdk.Promotion referencePromotion = this.API.Index.Promotions.GetById((Guid)sdkModel.promotion_id);
                if (referencePromotion != null)
                {
                    sdkModel.promotion_description = referencePromotion.promotion_description;
                }
                else
                {
                    Promotion referenceDomainPromotion = this.API.Direct.Promotions.GetById((Guid)sdkModel.promotion_id);
                    if (referenceDomainPromotion != null)
                    {
                        sdkModel.promotion_description = referenceDomainPromotion.promotion_description;
                    }
                }
            }
            
			
            
        }
        partial void HydrateSDKModel(Listing domainModel, sdk.Listing sdkModel);
    }
}

