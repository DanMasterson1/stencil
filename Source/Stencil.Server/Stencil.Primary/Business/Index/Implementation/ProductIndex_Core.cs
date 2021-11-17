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
    public partial class ProductIndex : IndexerBase<sdk.Product>, IProductIndex
    {
        public ProductIndex(IFoundation foundation)
            : base(foundation, "ProductIndex", DocumentNames.Product)
        {

        }
        protected override string GetModelId(sdk.Product model)
        {
            return model.product_id.ToString();
        }
        public ListResult<sdk.Product> GetByBrandId(Guid brand_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByBrandId", delegate ()
            {
                QueryContainer query = Query<sdk.Product>.Term(w => w.brand_id, brand_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Product>> sortFields = new List<SortFieldDescriptor<sdk.Product>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Product> item = new SortFieldDescriptor<sdk.Product>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Product> defaultSort = new SortFieldDescriptor<sdk.Product>()
                    .Field(r => r.product_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Product> searchResponse = client.Search<sdk.Product>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Product> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        
        public int GetCount(Guid brand_id)
        {
            return base.ExecuteFunction("GetCount", delegate ()
            {
                QueryContainer query = Query<Product>.Term(w => w.brand_id, brand_id);
                
                query &= Query<Product>.Exists(f => f.Field(x => x.brand_id));
               
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Product> response = client.Search<sdk.Product>(s => s
                    .Query(q => query)
                    .Skip(0)
                    .Take(0)
                    .Type(this.DocumentType));

                 
                return (int)response.GetTotalHit();
            });
        }
        
        public ListResult<sdk.Product> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false, Guid? brand_id = null)
        {
            return base.ExecuteFunction("Find", delegate ()
            {
                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                QueryContainer query = Query<sdk.Product>
                    .MultiMatch(m => m
                        .Query(keyword)
                        .Type(TextQueryType.PhrasePrefix)
                        .Fields(mf => mf
                                .Field(f => f.product_name)
                                .Field(f => f.product_description)
                ));
                                
                if(brand_id.HasValue)
                {
                    query &= Query<sdk.Product>.Term(f => f.brand_id, brand_id.Value);
                }
                
                
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
                ISearchResponse<sdk.Product> searchResponse = client.Search<sdk.Product>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(r => r.Field(order_by, sortOrder))
                    .Type(this.DocumentType));
                
                ListResult<sdk.Product> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
