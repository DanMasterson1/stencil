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

namespace Stencil.Plugins.RestAPI.Controllers
{
    public partial class OrderController
    {
        [HttpGet]
        [Route("get_outstanding")]
        public object GetOutstandingOrders(int skip, int take, decimal min_total, decimal min_lineitemcount, int created_daysback, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction(nameof(GetOutstandingOrders), delegate ()
            {
                ListResult<sdk.Order> result = this.API.Index.Orders.GetOutstandingOrders(skip, take, min_total, min_lineitemcount, created_daysback,  order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }

       

        //[HttpPut]
        //[Route("close/{order_id}")]
        //public object CloseOrder(Guid order_id)
        //{
        //    return base.ExecuteFunction(nameof(CloseOrder), delegate ()
        //    {
        //        dm.Order order = this.API.Direct.Orders.CloseOrder(order_id);

        //        sdk.Order existing = this.API.Index.Orders.GetById(order.order_id);

        //        return base.Http200(new ItemResult<sdk.Order>()
        //        {
        //            success = true,
        //            item = existing
        //        });
        //    });
        //}

    }
}