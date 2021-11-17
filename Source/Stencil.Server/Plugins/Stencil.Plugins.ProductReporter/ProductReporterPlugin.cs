using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Core;
using Codeable.Foundation.UI.Web.Common.Plugins;
using Codeable.Foundation.UI.Web.Core;
using Stencil.Plugins.ProductReporter.Integration;
using Stencil.Primary.Integration;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace Stencil.Plugins.ProductReporter
{
    public class ProductReporterPlugin : ChokeableClass, IWebPlugin
    {
        public ProductReporterPlugin()
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
                 //Am i registering these in the right place
                 this.IFoundation.Container.RegisterInstance<IProductNotify>(new ProductNotify(this.IFoundation));
                 this.IFoundation.Container.RegisterInstance<IProductSubscription>(new ProductSubscription(this.IFoundation));

                 ProductRegistration productRegistration = new ProductRegistration(this.IFoundation);
                 //this is where i register the plugin to the primary interface
                 productRegistration.RegisterSelf();

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
            get { return "ProductReporterPlugin"; }
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
