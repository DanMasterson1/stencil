using Stencil.SDK;
using sdk = Stencil.SDK.Models;
using Nest;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Stencil.Primary.Business.Index.Implementation
{
    public partial class ListingIndex
    {

        public ListResult<sdk.Listing> GetAssociatedListings(Guid brand_id)
        {
            return base.ExecuteFunction(nameof(GetAssociatedListings), delegate ()
            {
                QueryContainer query = Query<sdk.Listing>.Term(x => x.brand_id, brand_id);

                ElasticClient client = base.ClientFactory.CreateClient();
                ISearchResponse<sdk.Listing> searchResponse = client.Search<sdk.Listing>(s => s
                    .Query(q => query)
                    .Type(base.DocumentType)
                    );

                ListResult<sdk.Listing> result = searchResponse.Documents.ToSteppedListResult(0,searchResponse.GetTotalHit(),searchResponse.GetTotalHit());

                return result;

            });
        }
        public ListResult<sdk.Listing> GetPromotionalListingsForBrandInPriceRange(Guid promotion_id, int floor, int ceiling, string keyword = "")
        {
            return base.ExecuteFunction(nameof(GetPromotionalListingsForBrandInPriceRange), delegate ()
            {
                QueryContainer query = Query<sdk.Listing>
                .Bool(b => b
                        .Must(
                            m => m.Term(t => t.promotion_id,promotion_id),
                            m => m.Range(r => r
                                            .Field(f => f.listing_price)
                                            .GreaterThanOrEquals(floor)
                                            .LessThanOrEquals(ceiling)
                                        )
                         )
                        .MustNot(
                            m => m.Term(t => t.active,false)
                        )
                        .Should(
                            s => s.MultiMatch(k => k
                                    .Fields(f => f.Field( x => x.brand_name).Field("listing_description"))
                                    .Query(keyword)
                                  )
                        )
                        .MinimumShouldMatch(1)
                        
                );

                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Listing> searchResponse = client.Search<sdk.Listing>(s => s
                    .Query(q => query)
                    .Type(this.DocumentType)
                );

                ListResult<sdk.Listing> result = searchResponse.Documents.ToSteppedListResult(0, searchResponse.GetTotalHit(), searchResponse.GetTotalHit()); // if i want all of the results do I use GetTotalHit for take?

                return result;

            });
        }

        // must be a specific brand
        // promotion must be 50% off or greater or the price below 10$

        public ListResult<sdk.Listing> GetCloseOutDeals(Guid brand_id, Guid promotiond_id, int max_price)
        {
            return base.ExecuteFunction(nameof(GetCloseOutDeals), delegate ()
            {
                QueryContainer promotionQuery = Query<sdk.Promotion>.Term(f => f.promotion_id, promotiond_id);

                ElasticClient client = this.ClientFactory.CreateClient();
               
                ISearchResponse<sdk.Promotion> promotionSearchResponse = client.Search<sdk.Promotion>(s => s
                    .Query(q => promotionQuery)
                    .Type(DocumentNames.Promotion)
                    );

                decimal percentFloor = promotionSearchResponse.Documents.FirstOrDefault().percent;

                QueryContainer query = Query<sdk.Listing>
                    .Bool(b => b
                                .Must(m => m
                                       .Term(t => t.brand_id, brand_id)
                                )
                                .Should(
                                        s => s.Range(r => r.Field(f => f.promotion_percent).GreaterThanOrEquals((double?)percentFloor)),
                                        s => s.Range(r => r.Field(f => f.listing_price).LessThanOrEquals(max_price))
                                )
                                .MinimumShouldMatch(1)
                                
                    );

                
                
                List<SortFieldDescriptor<sdk.Listing>> sortFields = new List<SortFieldDescriptor<sdk.Listing>>();
                SortFieldDescriptor<sdk.Listing> item = new SortFieldDescriptor<sdk.Listing>()
                        .Field("listing_price")
                        .Order(SortOrder.Descending);

                sortFields.Add(item);

                ISearchResponse<sdk.Listing> listingSearchResponse = client.Search<sdk.Listing>(s => s
                        .Query(q => query)
                        .Sort(sr => sr.Multi(sortFields))
                        .Type(this.DocumentType)
                    );

                ListResult<sdk.Listing> result = listingSearchResponse.Documents.ToSteppedListResult(0, listingSearchResponse.GetTotalHit(), listingSearchResponse.GetTotalHit());

                return result;
            });
        }

        // if it has a promotion do something special if not dont

        public int GetCount(Guid brand_id)
        {
            return base.ExecuteFunction("GetCount", delegate ()
            {
                QueryContainer query = Query<sdk.Listing>.Term(w => w.brand_id, brand_id);

                query &= Query<sdk.Listing>.Exists(f => f.Field(x => x.brand_id));

                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Listing> response = client.Search<sdk.Listing>(s => s
                    .Query(q => query)
                    .Skip(0)
                    .Take(0)
                    .Type(this.DocumentType));


                return (int)response.GetTotalHit();
            });
        }

    }
}
