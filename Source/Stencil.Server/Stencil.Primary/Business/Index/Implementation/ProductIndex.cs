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
    public partial class ProductIndex
    {
        public ListResult<sdk.Product> GetProductsInPriceRange(int floor, int ceiling, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction(nameof(GetProductsInPriceRange), delegate ()
            {
                QueryContainer query = Query<sdk.Product>.Range(x => x.Field(p => p.baseprice).GreaterThanOrEquals(floor).LessThanOrEquals(ceiling));
                
                int takePlus = take;
                if (take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }

                List<SortFieldDescriptor<sdk.Product>> sortFields = new List<SortFieldDescriptor<sdk.Product>>();
                if (!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Product> item = new SortFieldDescriptor<sdk.Product>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);

                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Product> defaultSort = new SortFieldDescriptor<sdk.Product>()
                    .Field(r => r.baseprice)
                    .Ascending();

                sortFields.Add(defaultSort);

                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Product> searchResponse = client.Search<sdk.Product>(s => s
                .Query(q => query)
                .Skip(skip)
                .Take(takePlus)
                .Type(this.DocumentType)
                );

                ListResult<sdk.Product> result = searchResponse.Documents.ToSteppedListResult(skip,take,searchResponse.GetTotalHit());

                return result;

            });
        }

        public ItemResult<double> GetTotalSalesForProduct(Guid product_id)
        {
            return base.ExecuteFunction(nameof(GetTotalSalesForProduct), delegate ()
            {
                ElasticClient client = this.ClientFactory.CreateClient();

                //Get all listings for that brand
                QueryContainer listingQuery = Query<sdk.Listing>
                    .Term(x => x.product_id, product_id);

                ISearchResponse<sdk.Listing> listingSearchResponse = client.Search<sdk.Listing>(s => s
                    .Query(q => listingQuery)
                    .Type(DocumentNames.Listing)
                );

                Guid listingId = listingSearchResponse.Documents.Select(x => x.listing_id).FirstOrDefault();

                //Get all line items with those listings
                QueryContainer lineItemQuery = Query<sdk.LineItem>.Term(x => x.listing_id, listingId);

                ISearchResponse<sdk.LineItem> lineItemSearchResponse = client.Search<sdk.LineItem>(s => s
                    .Query(q => lineItemQuery)
                    .Aggregations(a => a
                       .Sum("product_total_sold", v => v.Field(x => x.lineitem_total))
                     )
                    .Type(DocumentNames.LineItem)
                );

                ItemResult<double> result = new ItemResult<double>();

                if (lineItemSearchResponse.GetTotalHit() > 0)
                {
                    double aggregateValue = (double)lineItemSearchResponse.Aggs.Sum("product_total_sold").Value;

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
