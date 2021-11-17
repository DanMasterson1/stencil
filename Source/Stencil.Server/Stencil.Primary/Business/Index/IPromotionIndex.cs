using Codeable.Foundation.Common;
using Stencil.SDK;
using sdk = Stencil.SDK.Models;
using Stencil.SDK.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Index
{
    partial interface IPromotionIndex
    {
        ListResult<sdk.Promotion> FindBy_DescriptionKeyword(int skip, int take, string keyword = "", string order_by = "", bool descending = false);
    }
}
