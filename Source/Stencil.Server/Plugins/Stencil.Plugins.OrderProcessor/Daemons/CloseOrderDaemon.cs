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
    public class CloseOrderDaemon : ChokeableClass, IDaemonTask
    {
        public CloseOrderDaemon(IFoundation iFoundation)
            : base(iFoundation)
        {
            this.API = iFoundation.Resolve<StencilAPI>();
        }

        #region IDaemonTask Members

        public const string DAEMON_NAME = "CloseOrderDaemon";

        protected static bool _executing;

        public StencilAPI API { get; set; }

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
                    this.CloseOrders();
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

        protected void CloseOrders()
        {
            base.ExecuteMethod(nameof(CloseOrders), delegate()
            {
               List<Guid> closeableOrders = this.API.Direct.Orders.GetCloseableOrders(); //TODO: non nullable
               
               foreach(Guid order_id in closeableOrders)
               {
                    if (this.API.Direct.Orders.IsCloseable(order_id))
                    {
                        this.API.Direct.Orders.CloseOrder(order_id);
                    }
               }
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
