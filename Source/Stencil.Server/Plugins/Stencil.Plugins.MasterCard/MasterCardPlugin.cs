﻿using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Common.Daemons;
using Codeable.Foundation.Core;
using Codeable.Foundation.UI.Web.Common.Plugins;
using Codeable.Foundation.UI.Web.Core;
using Stencil.Common;
using Stencil.Common.Configuration;
using Stencil.Common.Synchronization;
using Stencil.Plugins.MasterCard.Daemons;

using Stencil.Primary;
using Stencil.Primary.Daemons;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Stencil.Common.Integration;
using Stencil.Primary.Integration;
using Stencil.Plugins.MasterCard.Integration;

namespace Stencil.Plugins.MasterCard
{
    public class MasterCardPlugin : ChokeableClass, IWebPlugin
    {
        public MasterCardPlugin()
            : base(CoreFoundation.Current)
        {
        }

        public int DesiredRegistrationPriority
        {
            get { return 0; }
        }

        public void OnAfterWebPluginsRegistered(IEnumerable<IWebPlugin> allWebPlugins)
        {
        }

        public void OnAfterWebPluginsUnRegistered(IWebPlugin[] iWebPlugin)
        {
        }

        public void OnWebPluginRegistered(IWebPlugin plugin)
        {
        }

        public void OnWebPluginUnRegistered(IWebPlugin iWebPlugin)
        {
        }

        public void RegisterCustomRouting(System.Web.Routing.RouteCollection routes)
        {
            base.ExecuteMethod("RegisterCustomRouting", delegate ()
            {

                //ISettingsResolver settingsResolver = this.IFoundation.Resolve<ISettingsResolver>();
                PaymentProcessor paymentProcessor = new PaymentProcessor(this.IFoundation);
                this.IFoundation.Container.RegisterInstance<IProcessPayments>(paymentProcessor);

                IDaemonManager daemonManager = this.IFoundation.GetDaemonManager();
                DaemonConfig config = new DaemonConfig()
                {
                    InstanceName = string.Format(MasterCardDaemon.DAEMON_NAME_FORMAT, Agents.AGENT_DEFAULT),
                    ContinueOnError = true,
                    IntervalMilliSeconds = (int)TimeSpan.FromSeconds(180).TotalMilliseconds,
                    StartDelayMilliSeconds = 15,
                    TaskConfiguration = string.Empty
                };
                daemonManager.RegisterDaemon(config, new MasterCardDaemon(this.IFoundation, Agents.AGENT_DEFAULT), true);

                   
               
            });
        }

        public void RegisterLegacyOverrides(LegacyOverrideCollection overrides)
        {
        }

        public void UnRegisterCustomRouting(System.Web.Routing.RouteCollection routes)
        {
        }

        public void UnRegisterLegacyOverrides(LegacyOverrideCollection overrides)
        {
        }

        public bool WebInitialize(Codeable.Foundation.Common.IFoundation foundation, IDictionary<string, string> pluginConfig)
        {
            this.IFoundation = foundation;
            return true;
        }

        public string DisplayName
        {
            get { return "MasterCardPlugin"; }
        }

        public string DisplayVersion
        {
            get { return WebCoreUtility.GetInformationalVersion(Assembly.GetExecutingAssembly()); }
        }

        public object InvokeCommand(string name, Dictionary<string, object> caseInsensitiveParameters)
        {
            return null;
        }

        public T RetrieveMetaData<T>(string token)
        {
            return default(T);
        }
    }
}