using Codeable.Foundation.Common;
using Stencil.Common;
using Stencil.Domain;
using Stencil.SDK;
using sdk = Stencil.SDK.Models;
using dm = Stencil.Domain;
using Stencil.SDK.Models.Requests;
using Stencil.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Stencil.Primary;
using System.Threading.Tasks;
using Stencil.Primary.Mapping;

namespace Stencil.Plugins.RestAPI.Controllers
{
    public partial class BrandController
    {
        [HttpGet]
        [Route("avg_productprice")]
        public object GetAvgProductPrice(Guid brand_id)
        {
            return base.ExecuteFunction<object>(nameof(GetAvgProductPrice), delegate ()
            {
                ItemResult<double> result = this.API.Index.Brands.GetAvgProductPrice(brand_id);
                result.success = true;
                return base.Http200(result);
            });
        }

        [HttpGet]
        [Route("productcount_inrange")]
        public object GetProductCountByBrand(Guid brand_id,int floor, int ceiling)
        {
            return base.ExecuteFunction<object>(nameof(GetProductCountByBrand), delegate ()
            {
                ItemResult<int> result = this.API.Index.Brands.GetProductCountByBrand(brand_id,floor,ceiling);
                result.success = true;
                return base.Http200(result);
            });
        }

        [HttpGet]
        [Route("brand_totalsales/{brand_id}")]
        public object GetTotalsSalesForBrand(Guid brand_id)
        {
            return base.ExecuteFunction<object>(nameof(GetTotalsSalesForBrand), delegate ()
            {
                ItemResult<double> result = this.API.Index.Brands.GetTotalSales(brand_id);
                result.success = true;
                return base.Http200(result);
            });
        }

    }
}