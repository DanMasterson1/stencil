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
    public partial interface IListingIndex : IIndexer<Listing>
    {
        Listing GetById(Guid id);
        TCustomModel GetById<TCustomModel>(Guid id)
            where TCustomModel : class;
        ListResult<Listing> GetByBrandId(Guid brand_id, int skip, int take, string order_by = "", bool descending = false);
        ListResult<Listing> GetByProductId(Guid product_id, int skip, int take, string order_by = "", bool descending = false);
        ListResult<Listing> GetByPromotionId(Guid promotion_id, int skip, int take, string order_by = "", bool descending = false);
        
        ListResult<Listing> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false, Guid? brand_id = null, Guid? product_id = null, Guid? promotion_id = null);
       
    }
}
