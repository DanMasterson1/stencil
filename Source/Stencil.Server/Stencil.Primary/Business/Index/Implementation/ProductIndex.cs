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

        public ItemResult<double> GetTotalSalesOnOrder(Guid product_id, Guid order_id)
        {
            return base.ExecuteFunction(nameof(GetTotalSalesOnOrder), delegate ()
            {

                List<sdk.Listing> associatedListings = this.API.Index.Listings.GetByProductId(product_id,0,int.MaxValue).items;

                List<Guid> associatedListingIds = associatedListings.Select(x => x.listing_id).ToList();

                QueryContainer lineItemQuery = Query<sdk.LineItem>
                    .Terms(x => x
                            .Field(v => v.listing_id)
                            .Terms(associatedListingIds)
                    );

                lineItemQuery &= Query<sdk.LineItem>.Term(x => x.order_id,order_id);


                ElasticClient client = this.ClientFactory.CreateClient();

                ISearchResponse<sdk.LineItem> lineItemSearchResponse = client.Search<sdk.LineItem>(s => s
                    .Query(q => lineItemQuery)
                    .Type(DocumentNames.LineItem)
                    .Aggregations(a => a
                       .Sum("product_total_on_order", v => v.Field(x => x.lineitem_total))
                     )

                );
                //string response = System.Text.UTF8Encoding.Default.GetString(lineItemSearchResponse.ApiCall.ResponseBodyInBytes);

                ItemResult<double> result = new ItemResult<double>();

                if (lineItemSearchResponse.GetTotalHit() > 0)
                {
                    double aggregateValue = (double)lineItemSearchResponse.Aggs.Sum("product_total_on_order").Value;

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
