using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.SDK;

namespace Stencil.Primary.Business.Index
{
    public partial interface IBrandIndex
    {
        ItemResult<double> GetAvgProductPrice(Guid brand_id);
        ItemResult<int> GetProductCountByBrand(Guid brand_id, double floor, double ceiling);

        ItemResult<double> GetTotalSales(Guid brand_id);
    }
}
