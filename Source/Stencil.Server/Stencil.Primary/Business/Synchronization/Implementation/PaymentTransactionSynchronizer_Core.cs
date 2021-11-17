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
    public partial class PaymentTransactionSynchronizer : SynchronizerBase<Guid>, IPaymentTransactionSynchronizer
    {
        public PaymentTransactionSynchronizer(IFoundation foundation)
            : base(foundation, "PaymentTransactionSynchronizer")
        {

        }

        public override int Priority
        {
            get
            {
                return 60;
            }
        }

        public override void PerformSynchronizationForItem(Guid primaryKey)
        {
            base.ExecuteMethod("PerformSynchronizationForItem", delegate ()
            {
                PaymentTransaction domainModel = this.API.Direct.PaymentTransactions.GetById(primaryKey);
                if (domainModel != null)
                {
                    Action<Guid, bool, DateTime, string> synchronizationUpdateMethod = this.API.Direct.PaymentTransactions.SynchronizationUpdate;
                    if(this.API.Integration.SettingsResolver.IsHydrate())
                    {
                        synchronizationUpdateMethod = this.API.Direct.PaymentTransactions.SynchronizationHydrateUpdate;
                    }
                    DateTime syncDate = DateTime.UtcNow;
                    if (domainModel.sync_invalid_utc.HasValue)
                    {
                        syncDate = domainModel.sync_invalid_utc.Value;
                    }
                    try
                    {
                        sdk.PaymentTransaction sdkModel = domainModel.ToSDKModel();
                        
                        this.HydrateSDKModelComputed(domainModel, sdkModel);
                        this.HydrateSDKModel(domainModel, sdkModel);

                        if (domainModel.deleted_utc.HasValue)
                        {
                            this.API.Index.PaymentTransactions.DeleteDocument(sdkModel);
                            synchronizationUpdateMethod(domainModel.paymenttransaction_id, true, syncDate, null);
                        }
                        else
                        {
                            IndexResult result = this.API.Index.PaymentTransactions.UpdateDocument(sdkModel);
                            if (result.success)
                            {
                                synchronizationUpdateMethod(domainModel.paymenttransaction_id, true, syncDate, result.ToString());
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
                    invalidItems = this.API.Direct.PaymentTransactions.SynchronizationHydrateGetInvalid(CommonAssumptions.INDEX_RETRY_THRESHOLD_SECONDS, agentName);
                }
                else
                {
                    invalidItems = this.API.Direct.PaymentTransactions.SynchronizationGetInvalid(CommonAssumptions.INDEX_RETRY_THRESHOLD_SECONDS, agentName);
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
        protected void HydrateSDKModelComputed(PaymentTransaction domainModel, sdk.PaymentTransaction sdkModel)
        {
            
        }
        partial void HydrateSDKModel(PaymentTransaction domainModel, sdk.PaymentTransaction sdkModel);
    }
}
