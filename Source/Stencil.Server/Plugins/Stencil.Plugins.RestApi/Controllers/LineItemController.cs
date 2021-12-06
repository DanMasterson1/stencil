using Codeable.Foundation.Common;
using Codeable.Foundation.Core;
using System;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using sdk = Stencil.SDK.Models;
using dm = Stencil.Domain;
using Stencil.Primary;
using Stencil.SDK;
using Stencil.Web.Controllers;
using Stencil.Web.Security;
namespace Stencil.Plugins.RestAPI.Controllers
{
    public partial class LineItemController 
    {
        [HttpGet]
        [Route("average_lineitem_quantity")]
        public object GetAverageLineItemQuantity()
        {
            return base.ExecuteFunction<object>(nameof(GetAverageLineItemQuantity), delegate ()
            {
                ItemResult<int> result = this.API.Index.LineItems.GetAverageQuantity();
                result.success = true;
                return base.Http200(result);
            });
        }
    }
}