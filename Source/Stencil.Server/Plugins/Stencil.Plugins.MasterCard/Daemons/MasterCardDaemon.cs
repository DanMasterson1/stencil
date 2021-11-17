using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Common.Daemons;
using Codeable.Foundation.Core.Caching;
using Codeable.Foundation.Core.Unity;
using Stencil.Primary;
using Stencil.Plugins.MasterCard;
using Stencil.Plugins.MasterCard.Models;
using Stencil.Plugins.MasterCard.Integration;
using sdk = Stencil.SDK.Models;
using Stencil.Domain;

namespace Stencil.Plugins.MasterCard.Daemons
{
    public class MasterCardDaemon : ChokeableClass, IDaemonTask
    {
        public MasterCardDaemon(IFoundation iFoundation, string agentName)
           : base(iFoundation)
        {
            this.AgentName = agentName;
            this.API = iFoundation.Resolve<StencilAPI>();
            this.Cache = new AspectCache("MasterCardDaemon", iFoundation, new ExpireStaticLifetimeManager("MasterCardDaemon.Life15", System.TimeSpan.FromMinutes(15), false));
        }

        public StencilAPI API { get; set; }
        public AspectCache Cache { get; set; }

        public const string DAEMON_NAME_FORMAT = "MasterCardDaemon{0}";

        #region IDaemonTask Members


        public const string DAEMON_NAME = "MasterCardDaemon";

        protected static bool _executing;

        public string AgentName { get; set; }

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
            base.ExecuteMethod("Execute", delegate ()
            {
                if (_executing) { return; } // safety

                try
                {
                    _executing = true;
                    this.ProcessMasterCardPayments();
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

        protected void ProcessMasterCardPayments()
        {
            PaymentProcessor processor = new PaymentProcessor(IFoundation);
            processor.ProcessPayments();
        }
    }
}