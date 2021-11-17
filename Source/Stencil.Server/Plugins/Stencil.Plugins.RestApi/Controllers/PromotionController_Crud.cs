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
    [RoutePrefix("api/promotions")]
    public partial class PromotionController : HealthRestApiController
    {
        public PromotionController(IFoundation foundation)
            : base(foundation, "Promotion")
        {
        }

        [HttpGet]
        [Route("{promotion_id}")]
        public object GetById(Guid promotion_id)
        {
            return base.ExecuteFunction<object>("GetById", delegate()
            {
                sdk.Promotion result = this.API.Index.Promotions.GetById(promotion_id);
                if (result == null)
                {
                    return Http404("Promotion");
                }

                

                return base.Http200(new ItemResult<sdk.Promotion>()
                {
                    success = true, 
                    item = result
                });
            });
        }
        
        
        
        
       

        [HttpPost]
        [Route("")]
        public object Create(sdk.Promotion promotion)
        {
            return base.ExecuteFunction<object>("Create", delegate()
            {
                this.ValidateNotNull(promotion, "Promotion");

                dm.Promotion insert = promotion.ToDomainModel();

                
                insert = this.API.Direct.Promotions.Insert(insert);
                

                
                sdk.Promotion result = this.API.Index.Promotions.GetById(insert.promotion_id);

                return base.Http201(new ItemResult<sdk.Promotion>()
                {
                    success = true,
                    item = result
                }
                , string.Format("api/promotion/{0}", promotion.promotion_id));

            });

        }


        [HttpPut]
        [Route("{promotion_id}")]
        public object Update(Guid promotion_id, sdk.Promotion promotion)
        {
            return base.ExecuteFunction<object>("Update", delegate()
            {
                this.ValidateNotNull(promotion, "Promotion");
                this.ValidateRouteMatch(promotion_id, promotion.promotion_id, "Promotion");

                promotion.promotion_id = promotion_id;
                dm.Promotion update = promotion.ToDomainModel();


                update = this.API.Direct.Promotions.Update(update);
                
                
                sdk.Promotion existing = this.API.Index.Promotions.GetById(update.promotion_id);
                
                
                return base.Http200(new ItemResult<sdk.Promotion>()
                {
                    success = true,
                    item = existing
                });

            });

        }

        

        [HttpDelete]
        [Route("{promotion_id}")]
        public object Delete(Guid promotion_id)
        {
            return base.ExecuteFunction("Delete", delegate()
            {
                dm.Promotion delete = this.API.Direct.Promotions.GetById(promotion_id);
                
                
                this.API.Direct.Promotions.Delete(promotion_id);

                return Http200(new ActionResult()
                {
                    success = true,
                    message = promotion_id.ToString()
                });
            });
        }

    }
}

