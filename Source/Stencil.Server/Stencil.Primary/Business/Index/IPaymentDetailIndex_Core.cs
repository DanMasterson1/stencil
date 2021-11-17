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
    public partial interface IPaymentDetailIndex : IIndexer<PaymentDetail>
    {
        PaymentDetail GetById(Guid id);
        TCustomModel GetById<TCustomModel>(Guid id)
            where TCustomModel : class;
        ListResult<PaymentDetail> GetByAccountId(Guid account_id, int skip, int take, string order_by = "", bool descending = false);
        
    }
}
