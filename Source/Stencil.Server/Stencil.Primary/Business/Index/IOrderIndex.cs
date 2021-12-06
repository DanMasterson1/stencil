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
    partial interface IOrderIndex
    {
        ListResult<Order> GetOutstandingOrders(int skip, int take, decimal min_total = 0, decimal min_lineitemcount = 0, int created_daysback = 7, string order_by = "", bool descending = false);

        ListResult<Order> GetOrdersForProduct(Guid product_id);
    }
}
