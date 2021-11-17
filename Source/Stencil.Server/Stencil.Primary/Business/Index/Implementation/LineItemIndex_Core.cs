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
    public partial class LineItemIndex : IndexerBase<sdk.LineItem>, ILineItemIndex
    {
        public LineItemIndex(IFoundation foundation)
            : base(foundation, "LineItemIndex", DocumentNames.LineItem)
        {

        }
        protected override string GetModelId(sdk.LineItem model)
        {
            return model.lineitem_id.ToString();
        }
        public ListResult<sdk.LineItem> GetByOrderId(Guid order_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByOrderId", delegate ()
            {
                QueryContainer query = Query<sdk.LineItem>.Term(w => w.order_id, order_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.LineItem>> sortFields = new List<SortFieldDescriptor<sdk.LineItem>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.LineItem> item = new SortFieldDescriptor<sdk.LineItem>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.LineItem> defaultSort = new SortFieldDescriptor<sdk.LineItem>()
                    .Field(r => r.lineitem_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.LineItem> searchResponse = client.Search<sdk.LineItem>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.LineItem> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.LineItem> GetByListingId(Guid listing_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByListingId", delegate ()
            {
                QueryContainer query = Query<sdk.LineItem>.Term(w => w.listing_id, listing_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.LineItem>> sortFields = new List<SortFieldDescriptor<sdk.LineItem>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.LineItem> item = new SortFieldDescriptor<sdk.LineItem>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.LineItem> defaultSort = new SortFieldDescriptor<sdk.LineItem>()
                    .Field(r => r.lineitem_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.LineItem> searchResponse = client.Search<sdk.LineItem>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.LineItem> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        
        public int GetCount(Guid order_id)
        {
            return base.ExecuteFunction("GetCount", delegate ()
            {
                QueryContainer query = Query<LineItem>.Term(w => w.order_id, order_id);
                
                query &= Query<LineItem>.Exists(f => f.Field(x => x.order_id));
               
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.LineItem> response = client.Search<sdk.LineItem>(s => s
                    .Query(q => query)
                    .Skip(0)
                    .Take(0)
                    .Type(this.DocumentType));

                 
                return (int)response.GetTotalHit();
            });
        }
        

    }
}
