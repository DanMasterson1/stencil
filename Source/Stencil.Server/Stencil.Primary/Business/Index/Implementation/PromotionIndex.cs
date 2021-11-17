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
    public partial class PromotionIndex
    {
        public ListResult<sdk.Promotion> FindBy_DescriptionKeyword(int skip, int take, string keyword = "", string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction(nameof(FindBy_DescriptionKeyword), delegate ()
            {
                QueryContainer query = Query<sdk.Promotion>.MatchPhrase(m => m.Field(f => f.promotion_description).Query(keyword));

                int takePlus = take;
                if (take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }

                List<SortFieldDescriptor<sdk.Promotion>> sortFields = new List<SortFieldDescriptor<sdk.Promotion>>();
                if (!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Promotion> item = new SortFieldDescriptor<sdk.Promotion>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);

                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Promotion> defaultSort = new SortFieldDescriptor<sdk.Promotion>()
                    .Field(r => r.percent)
                    .Ascending();

                sortFields.Add(defaultSort);

                ElasticClient client = this.ClientFactory.CreateClient();

                ISearchResponse<sdk.Promotion> searchResponse = client.Search<sdk.Promotion>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(take)
                    .Sort(o => o.Multi(sortFields))
                    .Type(this.DocumentType)
                );

                ListResult<sdk.Promotion> result = searchResponse.Documents.ToSteppedListResult(skip,take,searchResponse.GetTotalHit());

                return result;
            });
        }
    }
}
