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
    partial class LineItemIndex
    {
        public ItemResult<int> GetAverageQuantity()
        {
            return base.ExecuteFunction(nameof(GetAverageQuantity), delegate ()
            {
                ElasticClient client = this.ClientFactory.CreateClient();

                ISearchResponse<sdk.LineItem> searchResponse = client.Search<sdk.LineItem>(s => s
                .Query(q => q.MatchAll())
                .Type(DocumentNames.LineItem)
                .Aggregations(a => a
                    .Average("average_quantity", v => v.Field(f => f.lineitem_quantity))
                    )
                );

                ItemResult<int> result = new ItemResult<int>();

                result.item = (int)searchResponse.Aggs.Average("average_quantity").Value;

                result.success = true;

                return result;
            });
        }
    }
}
