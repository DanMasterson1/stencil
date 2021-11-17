using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Common.Daemons;
using Codeable.Foundation.Core.Caching;
using Codeable.Foundation.Core.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Stencil.Common.Configuration;
using Stencil.Common;
using Stencil.Domain;
using System.Data.Sql;
using Stencil.Primary;
using Stencil.Primary.Business.Direct.Implementation;


namespace Stencil.Plugins.OrderProcessor
{
    public class ExpiryDaemon : ChokeableClass, IDaemonTask
    {
        public ExpiryDaemon(IFoundation iFoundation)
            : base(iFoundation)
        {
        }

        #region IDaemonTask Members

        public const string DAEMON_NAME = "ExpiryDaemon";

        protected static bool _executing;

        public string DaemonName
        {
            get
            {
                return DAEMON_NAME;
            }
            protected set
            {
            }
        }

        public void Execute(Codeable.Foundation.Common.IFoundation iFoundation)
        {
            base.ExecuteMethod("Execute", delegate()
            {
                if (_executing) { return; } // safety

                try
                {
                    _executing = true;
                    this.ExpireListings();
                }
                finally
                {
                    _executing = false;
                }
            });
        }

        public DaemonSynchronizationPolicy SynchronizationPolicy
        {
            get { return DaemonSynchronizationPolicy.SingleAppDomain; }
        }

        #endregion

        protected void ExpireListings()
        {
            base.ExecuteMethod(nameof(ExpireListings), delegate()
            {
                StencilAPI api = new StencilAPI(IFoundation);
               
               api.Direct.Listings.InvalidateExpiredListings();
               
               
            });
        }


        protected virtual string ApiKey
        {
            get
            {
                ISettingsResolver settings = this.IFoundation.Resolve<ISettingsResolver>();
                return settings.GetSetting(CommonAssumptions.APP_KEY_HEALTH_APIKEY);
            }
        }
    }
}
