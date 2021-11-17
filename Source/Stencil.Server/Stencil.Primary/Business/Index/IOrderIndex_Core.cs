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
    public partial interface IOrderIndex : IIndexer<Order>
    {
        Order GetById(Guid id);
        TCustomModel GetById<TCustomModel>(Guid id)
            where TCustomModel : class;
        ListResult<Order> GetByAccountId(Guid account_id, int skip, int take, string order_by = "", bool descending = false);
        ListResult<Order> GetByInvoiceId(Guid invoice_id, int skip, int take, string order_by = "", bool descending = false);
        ListResult<Order> GetByPaymentId(Guid payment_id, int skip, int take, string order_by = "", bool descending = false);
        ListResult<Order> GetByShipmentId(Guid shipment_id, int skip, int take, string order_by = "", bool descending = false);
        
    }
}
