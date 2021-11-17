using System;
using System.Collections.Generic;
using System.Text;
using Stencil.Domain;

namespace Stencil.Primary.Business.Direct
{
    partial interface IOrderBusiness
    {
        Order CloseOrder(Guid order_id);

        List<Guid> GetNonInvoicedOrders();

        List<Guid> GetCloseableOrders();

        bool IsCloseable(Guid order_id);
    }
}
