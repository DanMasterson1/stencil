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
    partial interface IProductIndex
    {
        ListResult<Product> GetProductsInPriceRange(int floor, int ceiling, int skip, int take, string order_by = "", bool descending = false);

        // ListResult<Product> GetMatchedProductsInPriceRange(int floor, int ceiling,  int skip, int take, string keyword = "", string order_by = "", bool descending = false);

        ItemResult<double> GetTotalSalesForProduct(Guid product_id);

        int GetCount(Guid brand_id);

        ItemResult<double> GetTotalSalesOnOrder(Guid product_id, Guid order_id);

    }
}
