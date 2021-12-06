using Codeable.Foundation.Common;
using Stencil.SDK.Models;
using Stencil.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Index
{
    partial interface ILineItemIndex
    {
        // LineItem GetActiveById(Guid id);
        ItemResult<int> GetAverageQuantity();
    }
}
