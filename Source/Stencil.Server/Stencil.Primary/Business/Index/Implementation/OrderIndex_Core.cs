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
    public partial class OrderIndex : IndexerBase<sdk.Order>, IOrderIndex
    {
        public OrderIndex(IFoundation foundation)
            : base(foundation, "OrderIndex", DocumentNames.Order)
        {

        }
        protected override string GetModelId(sdk.Order model)
        {
            return model.order_id.ToString();
        }
        public ListResult<sdk.Order> GetByAccountId(Guid account_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByAccountId", delegate ()
            {
                QueryContainer query = Query<sdk.Order>.Term(w => w.account_id, account_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Order>> sortFields = new List<SortFieldDescriptor<sdk.Order>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Order> item = new SortFieldDescriptor<sdk.Order>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Order> defaultSort = new SortFieldDescriptor<sdk.Order>()
                    .Field(r => r.order_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Order> searchResponse = client.Search<sdk.Order>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Order> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.Order> GetByInvoiceId(Guid invoice_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByInvoiceId", delegate ()
            {
                QueryContainer query = Query<sdk.Order>.Term(w => w.invoice_id, invoice_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Order>> sortFields = new List<SortFieldDescriptor<sdk.Order>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Order> item = new SortFieldDescriptor<sdk.Order>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Order> defaultSort = new SortFieldDescriptor<sdk.Order>()
                    .Field(r => r.order_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Order> searchResponse = client.Search<sdk.Order>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Order> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.Order> GetByPaymentId(Guid payment_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByPaymentId", delegate ()
            {
                QueryContainer query = Query<sdk.Order>.Term(w => w.payment_id, payment_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Order>> sortFields = new List<SortFieldDescriptor<sdk.Order>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Order> item = new SortFieldDescriptor<sdk.Order>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Order> defaultSort = new SortFieldDescriptor<sdk.Order>()
                    .Field(r => r.order_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Order> searchResponse = client.Search<sdk.Order>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Order> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.Order> GetByShipmentId(Guid shipment_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByShipmentId", delegate ()
            {
                QueryContainer query = Query<sdk.Order>.Term(w => w.shipment_id, shipment_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Order>> sortFields = new List<SortFieldDescriptor<sdk.Order>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Order> item = new SortFieldDescriptor<sdk.Order>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Order> defaultSort = new SortFieldDescriptor<sdk.Order>()
                    .Field(r => r.order_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Order> searchResponse = client.Search<sdk.Order>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Order> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
