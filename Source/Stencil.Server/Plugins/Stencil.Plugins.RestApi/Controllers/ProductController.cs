using Codeable.Foundation.Common;
using Codeable.Foundation.Core;
using System;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using sdk = Stencil.SDK.Models;
using dm = Stencil.Domain;
using Stencil.Primary;
using Stencil.SDK;
using Stencil.Web.Controllers;
using Stencil.Web.Security;
using Stencil.Primary.Workers.Models;
using Newtonsoft.Json;
using Stencil.Primary.Workers;
using Stencil.Plugins;
using Stencil.Primary.Models;


namespace Stencil.Plugins.RestAPI.Controllers
{
    public partial class ProductController
    {
        [HttpGet]
        [Route("by_pricerange")]
        public object GetProductsInPriceRange(int floor, int ceiling,int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>(nameof(GetProductsInPriceRange), delegate ()
            {
                ListResult<sdk.Product> result = this.API.Index.Products.GetProductsInPriceRange(floor, ceiling, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }

        [HttpGet]
        [Route("product_totalsales/{product_id}")]
        public object GetTotalsSalesForProduct(Guid product_id)
        {
            return base.ExecuteFunction<object>(nameof(GetTotalsSalesForProduct), delegate ()
            {
                ItemResult<double> result = this.API.Index.Products.GetTotalSalesForProduct(product_id);
                result.success = true;
                return base.Http200(result);
            });
        }

        [HttpGet]
        [Route("{product_id}")]
        public object GetById(Guid product_id, bool report_request)
        {
            return base.ExecuteFunction<object>("GetById", delegate ()
            {
                if (report_request)
                {
                    sdk.Product result = this.API.Index.Products.GetById(product_id);

                    QueryReportRequest request = new QueryReportRequest()
                    {
                        entity = this.TrackPrefix,
                        query_context = nameof(GetById),
                        // keyword that was searched
                    };

                    if (result == null)
                    {
                        request.response = JsonConvert.SerializeObject(Http404(this.TrackPrefix));

                        QueryReportWorker.EnqueueRequest(base.IFoundation, request);
                        
                        return Http404("Product");
                    }

                    request.response = JsonConvert.SerializeObject(result);

                    QueryReportWorker.EnqueueRequest(base.IFoundation, request);

                    return base.Http200(new ItemResult<sdk.Product>()
                    {
                        success = true,
                        item = result
                    });
                }
                else
                {
                    return this.GetById(product_id);
                }

                
            });
        }
    }
}