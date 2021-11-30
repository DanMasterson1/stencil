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
    public partial class ListingIndex : IndexerBase<sdk.Listing>, IListingIndex
    {
        public ListingIndex(IFoundation foundation)
            : base(foundation, "ListingIndex", DocumentNames.Listing)
        {

        }
        protected override string GetModelId(sdk.Listing model)
        {
            return model.listing_id.ToString();
        }
        public ListResult<sdk.Listing> GetByBrandId(Guid brand_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByBrandId", delegate ()
            {
                QueryContainer query = Query<sdk.Listing>.Term(w => w.brand_id, brand_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Listing>> sortFields = new List<SortFieldDescriptor<sdk.Listing>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Listing> item = new SortFieldDescriptor<sdk.Listing>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Listing> defaultSort = new SortFieldDescriptor<sdk.Listing>()
                    .Field(r => r.listing_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Listing> searchResponse = client.Search<sdk.Listing>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Listing> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.Listing> GetByProductId(Guid product_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByProductId", delegate ()
            {
                QueryContainer query = Query<sdk.Listing>.Term(w => w.product_id, product_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Listing>> sortFields = new List<SortFieldDescriptor<sdk.Listing>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Listing> item = new SortFieldDescriptor<sdk.Listing>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Listing> defaultSort = new SortFieldDescriptor<sdk.Listing>()
                    .Field(r => r.listing_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Listing> searchResponse = client.Search<sdk.Listing>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Listing> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.Listing> GetByPromotionId(Guid promotion_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByPromotionId", delegate ()
            {
                QueryContainer query = Query<sdk.Listing>.Term(w => w.promotion_id, promotion_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Listing>> sortFields = new List<SortFieldDescriptor<sdk.Listing>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Listing> item = new SortFieldDescriptor<sdk.Listing>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Listing> defaultSort = new SortFieldDescriptor<sdk.Listing>()
                    .Field(r => r.listing_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Listing> searchResponse = client.Search<sdk.Listing>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Listing> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        
        public ListResult<sdk.Listing> Find(int skip, int take, string keyword = "", string order_by = "", bool descending = false, Guid? brand_id = null, Guid? product_id = null, Guid? promotion_id = null)
        {
            return base.ExecuteFunction("Find", delegate ()
            {
                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                QueryContainer query = Query<sdk.Listing>
                    .MultiMatch(m => m
                        .Query(keyword)
                        .Type(TextQueryType.PhrasePrefix)
                        .Fields(mf => mf
                                .Field(f => f.listing_description)
                ));
                                
                if(brand_id.HasValue)
                {
                    query &= Query<sdk.Listing>.Term(f => f.brand_id, brand_id.Value);
                }
                if(product_id.HasValue)
                {
                    query &= Query<sdk.Listing>.Term(f => f.product_id, product_id.Value);
                }
                if(promotion_id.HasValue)
                {
                    query &= Query<sdk.Listing>.Term(f => f.promotion_id, promotion_id.Value);
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
                ISearchResponse<sdk.Listing> searchResponse = client.Search<sdk.Listing>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(r => r.Field(order_by, sortOrder))
                    .Type(this.DocumentType));
                
                ListResult<sdk.Listing> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
