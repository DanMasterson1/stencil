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
    public partial class PromotionController 
    {
       [HttpGet]
       [Route("by_descriptionkeyword")]
       public ListResult<sdk.Promotion> FindBy_DescriptionKeyword(int skip, int take, string keyword = "", string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction(nameof(FindBy_DescriptionKeyword), delegate ()
            {
                ListResult<sdk.Promotion> result = this.API.Index.Promotions.FindBy_DescriptionKeyword(skip, take, keyword, order_by, descending);
                result.success = true;
                return result;
            });
        }
    }
}