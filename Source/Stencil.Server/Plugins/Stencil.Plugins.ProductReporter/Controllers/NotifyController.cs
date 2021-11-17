using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Stencil.Primary.Integration;

namespace Stencil.Plugins.ProductReporter.Controllers
{
    public class NotifyController : Controller, IProductNotify
    {
        [System.Web.Http.HttpPost]
        public async Task<bool> Notify()
        {
            // Create an event with action 'ProductQueried' and additional data
            await this.NotifyAllAsync("ProductQueried", new { P1 = "p1" });
            // persist those subscribers (the brands)s\
            return true;
        }
    }
}