using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Common.Daemons;
using Codeable.Foundation.Core;
using Codeable.Foundation.UI.Web.Common.Plugins;
using Codeable.Foundation.UI.Web.Core;
using Stencil.Common;
using Stencil.Common.Configuration;
using Stencil.Common.Synchronization;
using Stencil.Primary;
using Stencil.Primary.Daemons;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Stencil.Common.Integration;


namespace Stencil.Plugins.OrderProcessor
{
    public class OrderProcessorPlugin : ChokeableClass, IWebPlugin
    {
        public OrderProcessorPlugin()
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

                IDaemonManager daemonManager = this.IFoundation.GetDaemonManager();

                DaemonConfig expiryConfig = new DaemonConfig()
                {
                    InstanceName = ExpiryDaemon.DAEMON_NAME,
                    ContinueOnError = true,
                    IntervalMilliSeconds = 15 * 1000, // every 15 seconds
                    StartDelayMilliSeconds = 60 * 1000,
                    TaskConfiguration = string.Empty
                };
                daemonManager.RegisterDaemon(expiryConfig, new ExpiryDaemon(this.IFoundation), true);

                DaemonConfig closeOrderConfig = new DaemonConfig()
                {
                    InstanceName = CloseOrderDaemon.DAEMON_NAME,
                    ContinueOnError = true,
                    IntervalMilliSeconds = 15 * 1000, // every 15 seconds
                    StartDelayMilliSeconds = 60 * 1000,
                    TaskConfiguration = string.Empty
                };
                daemonManager.RegisterDaemon(closeOrderConfig, new CloseOrderDaemon(this.IFoundation), true);



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
            get { return "OrderProcessorPlugin"; }
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
