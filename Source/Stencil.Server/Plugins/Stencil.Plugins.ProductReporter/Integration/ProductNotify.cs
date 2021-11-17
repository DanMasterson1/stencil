using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Stencil.Primary;
using Stencil.Primary.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Stencil.Plugins.ProductReporter.Integration
{
    public class ProductNotify : ChokeableClass, IProductNotify
    {
        public ProductNotify(IFoundation foundation)
          : base(foundation)
        {
            this.API = foundation.Resolve<StencilAPI>();
            
        }

        public object API { get; private set; }

        public async Task<bool> Notify()
        {
            //await a call to the controller

            await Task.Run(() => { });
            return true;
        }
    }
}