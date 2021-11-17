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
    [ApiKeyHttpAuthorize]
    [RoutePrefix("api/shipments")]
    public partial class ShipmentController : HealthRestApiController
    {
        public ShipmentController(IFoundation foundation)
            : base(foundation, "Shipment")
        {
        }

        [HttpGet]
        [Route("{shipment_id}")]
        public object GetById(Guid shipment_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Shipment result = this.API.Index.Shipments.GetById(shipment_id);
                if (result == null)
                {
                    return Http404("Shipment");
                }

                

                return base.Http200(new ItemResult<sdk.Shipment>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        [HttpGet]
        [Route("by_orderid/{order_id}")]
        public object GetByOrderId(Guid order_id, int skip = 0, int take = 10, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction<object>("GetByOrderId", delegate ()
            {
                
                
                ListResult<sdk.Shipment> result = this.API.Index.Shipments.GetByOrderId(order_id, skip, take, order_by, descending);
                result.success = true;
                return base.Http200(result);
            });
        }
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Shipment shipment)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(shipment, "Shipment");

                dm.Shipment insert = shipment.ToDomainModel();

                
                insert = this.API.Direct.Shipments.Insert(insert);
                

                
                sdk.Shipment result = this.API.Index.Shipments.GetById(insert.shipment_id);

                return base.Http201(new ItemResult<sdk.Shipment>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/shipment/{0}", shipment.shipment_id));

            });

        }


        [HttpPut]
        [Route("{shipment_id}")]
        public object Update(Guid shipment_id, sdk.Shipment shipment)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(shipment, "Shipment");
                this.ValidateRouteMatch(shipment_id, shipment.shipment_id, "Shipment");

                shipment.shipment_id = shipment_id;
                dm.Shipment update = shipment.ToDomainModel();


                update = this.API.Direct.Shipments.Update(update);
                
                
                sdk.Shipment existing = this.API.Index.Shipments.GetById(update.shipment_id);
                
                
                return base.Http200(new ItemResult<sdk.Shipment>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{shipment_id}")]
        public object Delete(Guid shipment_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Shipment delete = this.API.Direct.Shipments.GetById(shipment_id);
                
                
                this.API.Direct.Shipments.Delete(shipment_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = shipment_id.ToString()
                });
            });
        }

    }
}

