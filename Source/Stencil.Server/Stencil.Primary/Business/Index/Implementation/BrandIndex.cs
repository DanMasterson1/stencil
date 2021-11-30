using Stencil.SDK;
using sdk = Stencil.SDK.Models;
using Nest;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Stencil.Primary.Business.Index.Implementation
{
    public partial class BrandIndex
    {
       public ItemResult<double> GetAvgProductPrice(Guid brand_id)
        {
            return base.ExecuteFunction(nameof(GetAvgProductPrice), delegate ()
            {
                QueryContainer query = Query<sdk.Product>.Term(x => x.brand_id, brand_id);

                ElasticClient client = base.ClientFactory.CreateClient();
                ISearchResponse<sdk.Product> searchResponse = client.Search<sdk.Product>(s => s
                    .Query(q => query)
                    .Aggregations(a => a.Average("avg_baseprice",avg => avg.Field("baseprice")))
                    .Type(DocumentNames.Product)
                );

                ItemResult<double> result = new ItemResult<double>();

                if (searchResponse.GetTotalHit() > 0)
                {
                    double aggregateValue = (double)searchResponse.Aggs.Average("avg_baseprice").Value;

                    result.item = aggregateValue;
                    
                }
                else
                {
                    
                    result.item = 0;
                    result.message = "No products exist with this brand id.";
                }

                result.success = true;
                return result;
            });
        }

       public ItemResult<int> GetProductCountByBrand(Guid brand_id, double floor, double ceiling)
        {
            return base.ExecuteFunction(nameof(GetProductCountByBrand), delegate ()
            {
                QueryContainer query = Query<sdk.Product>.Term(x => x.brand_id, brand_id);

                ElasticClient client = base.ClientFactory.CreateClient();
                ISearchResponse<sdk.Product> searchResponse = client.Search<sdk.Product>(s => s
                    .Query(q => query)
                    .Type(DocumentNames.Product)
                    .Aggregations(a => a
                        .Range("product_countinrange", ra => ra
                            .Field(f => f.baseprice)
                            .Ranges(r => r.From(floor).To(ceiling))
                        )
                    )
                    
                );

                ItemResult<int> result = new ItemResult<int>();

                if(searchResponse.GetTotalHit() > 0)
                {
                    MultiBucketAggregate<RangeBucket> range = searchResponse.Aggs.Range("product_countinrange");
                    RangeBucket bucket = range.Buckets.FirstOrDefault(); 
                    
                    int aggregateValue = (int)bucket.DocCount;
                    result.item = aggregateValue;
                }
                else
                {
                    result.item = 0;
                }

                return result;
            });
        }

        public ItemResult<double> GetTotalSales(Guid brand_id)
        {
            return base.ExecuteFunction(nameof(GetTotalSales), delegate ()
            {

                List<sdk.Listing> associatedListings = this.API.Index.Listings.GetAssociatedListings(brand_id).items;

                List<Guid> associatedListingIds = associatedListings.Select(x => x.listing_id).ToList();

                QueryContainer lineItemQuery = Query<sdk.LineItem>
                    .Terms(x => x
                            .Field(v => v.listing_id)
                            .Terms(associatedListingIds)
                    );

                ElasticClient client = this.ClientFactory.CreateClient();

                ISearchResponse<sdk.LineItem> lineItemSearchResponse = client.Search<sdk.LineItem>(s => s
                    .Query(q => lineItemQuery)
                    .Type(DocumentNames.LineItem)
                    .Aggregations(a => a
                       .Sum("brand_total_sold",v => v.Field(x => x.lineitem_total))
                     )
                    
                );
                //string response = System.Text.UTF8Encoding.Default.GetString(lineItemSearchResponse.ApiCall.ResponseBodyInBytes);

                ItemResult<double> result = new ItemResult<double>();

                if (lineItemSearchResponse.GetTotalHit() > 0)
                {
                    double aggregateValue = (double)lineItemSearchResponse.Aggs.Sum("brand_total_sold").Value;

                    result.item = aggregateValue;

                }
                else
                {

                    result.item = 0;
                }

                result.success = true;
                return result;
            });
        }

       
    }
}
