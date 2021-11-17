using Codeable.Foundation.Common;
using Stencil.SDK;
using sdk = Stencil.SDK.Models;
using Stencil.SDK.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Index.Implementation
{
    public partial class PromotionIndex : IndexerBase<sdk.Promotion>, IPromotionIndex
    {
        public PromotionIndex(IFoundation foundation)
            : base(foundation, "PromotionIndex", DocumentNames.Promotion)
        {

        }
        protected override string GetModelId(sdk.Promotion model)
        {
            return model.promotion_id.ToString();
        }
        

    }
}
