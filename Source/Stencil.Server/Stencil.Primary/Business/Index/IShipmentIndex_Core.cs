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
    public partial interface IShipmentIndex : IIndexer<Shipment>
    {
        Shipment GetById(Guid id);
        TCustomModel GetById<TCustomModel>(Guid id)
            where TCustomModel : class;
        ListResult<Shipment> GetByOrderId(Guid order_id, int skip, int take, string order_by = "", bool descending = false);
        
    }
}
