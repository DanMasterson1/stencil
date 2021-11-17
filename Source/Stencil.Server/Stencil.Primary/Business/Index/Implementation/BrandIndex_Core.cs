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
    public partial class BrandIndex : IndexerBase<sdk.Brand>, IBrandIndex
    {
        public BrandIndex(IFoundation foundation)
            : base(foundation, "BrandIndex", DocumentNames.Brand)
        {

        }
        protected override string GetModelId(sdk.Brand model)
        {
            return model.brand_id.ToString();
        }
        
        public ListResult<sdk.Brand> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("Find", delegate ()
            {
                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                QueryContainer query = Query<sdk.Brand>
                    .MultiMatch(m => m
                        .Query(keyword)
                        .Type(TextQueryType.PhrasePrefix)
                        .Fields(mf => mf
                                .Field(f => f.brand_name)
                ));
                                
                
                
                SortOrder sortOrder = SortOrder.Ascending;
                if (descending)
                {
                    sortOrder = SortOrder.Descending;
                }
                if (string.IsNullOrEmpty(order_by))
                {
                    order_by = "";
                }

                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Brand> searchResponse = client.Search<sdk.Brand>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(r => r.Field(order_by, sortOrder))
                    .Type(this.DocumentType));
                
                ListResult<sdk.Brand> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
