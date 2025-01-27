﻿using Codeable.Foundation.Common;
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
    public partial class OrderIndex
    {
        public ListResult<sdk.Order> GetOutstandingOrders(int skip, int take, decimal min_total, decimal min_lineitemcount, int created_daysback, string order_by, bool descending)
        {
            return base.ExecuteFunction(nameof(GetOutstandingOrders), delegate ()
            {
                // Query -> ((OrderPaid = false AND OrderShipped = false) AND (OrderTotal >= min_total OR LineItemCount >= min_lineitemcount)) AND created >= (-)daysback

                //QueryContainer query = Query<sdk.Order>.Bool(b => b
                //            .Must(
                //                m => m.Bool(o => o
                //                            .Must( 
                //                                v => v.MultiMatch(u => u
                //                                                    .Fields(d => d.Field(f => f.order_paid).Field("order_shipped"))
                //                                                    .Query("false")
                //                                                )
                //                            )
                //                            .Should( // think about using &=
                //                                h => h.Range(x => x.Field(f => f.order_total).GreaterThanOrEquals((double)min_total)),
                //                                h => h.Range(x => x.Field(f => f.lineitem_count).GreaterThanOrEquals((double)min_lineitemcount))
                //                            )
                //                            .MinimumShouldMatch(1)
                //                         ),
                //                m => m.DateRange(x => x.Field(f => f.created_utc).GreaterThanOrEquals(DateTime.UtcNow.AddDays(-created_daysback)))
                //            )
                //      );

                QueryContainer query = Query<sdk.Order>.Bool(o => o
                                            .Must(
                                                v => v.MultiMatch(u => u
                                                                    .Fields(d => d.Field(f => f.order_paid).Field("order_shipped"))
                                                                    .Query("false")
                                                                )
                                            )
                                         );

                query &= Query<sdk.Order>.Bool(o => o
                                            .Should(
                                                h => h.Range(x => x.Field(f => f.order_total).GreaterThanOrEquals((double)min_total)),
                                                h => h.Range(x => x.Field(f => f.lineitem_count).GreaterThanOrEquals((double)min_lineitemcount))
                                            )
                                            .MinimumShouldMatch(1)
                                         );

                query &= Query<sdk.Order>.DateRange(x => x.Field(f => f.created_utc).GreaterThanOrEquals(DateTime.UtcNow.AddDays(-created_daysback)));

                int takePlus = take;
                if (take != int.MaxValue)
                {
                    takePlus++;
                }

                List<SortFieldDescriptor<sdk.Order>> sortFields = new List<SortFieldDescriptor<sdk.Order>>();
                if (!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Order> item = new SortFieldDescriptor<sdk.Order>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);

                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Order> defaultSort = new SortFieldDescriptor<sdk.Order>()
                    .Field(r => r.order_total)
                    .Ascending();

                sortFields.Add(defaultSort);

                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Order> searchResponse = client.Search<sdk.Order>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(take)
                    .Sort(r => r.Multi(sortFields))
                    .Type(this.DocumentType)
                    );

                ListResult<sdk.Order> result = searchResponse.Documents.ToSteppedListResult(skip,take,searchResponse.GetTotalHit());

                return result;
            });
        }

        public ListResult<sdk.Order> GetOrdersForProduct(Guid product_id)
        {
            return base.ExecuteFunction(nameof(GetOrdersForProduct), delegate ()
            {
                //sdk.Product referenceProduct = this.API.Index.Products.GetById(product_id);
                //List<sdk.Product> productList = new List<Product>();

                //productList.Add(referenceProduct);

                QueryContainer query = Query<sdk.Order>.Term("products.product_id",product_id);

                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Order> response = client.Search<sdk.Order>(s => s
                .Query(q => query)
                .Type(DocumentNames.Order)
                );

                ListResult<sdk.Order> result = response.Documents.ToSteppedListResult(0, int.MaxValue);

                return result;
            });
        }
    }
}
