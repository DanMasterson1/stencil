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
    public partial class PaymentTransactionIndex : IndexerBase<sdk.PaymentTransaction>, IPaymentTransactionIndex
    {
        public PaymentTransactionIndex(IFoundation foundation)
            : base(foundation, "PaymentTransactionIndex", DocumentNames.PaymentTransaction)
        {

        }
        protected override string GetModelId(sdk.PaymentTransaction model)
        {
            return model.paymenttransaction_id.ToString();
        }
        public ListResult<sdk.PaymentTransaction> GetByOrderId(Guid order_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByOrderId", delegate ()
            {
                QueryContainer query = Query<sdk.PaymentTransaction>.Term(w => w.order_id, order_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.PaymentTransaction>> sortFields = new List<SortFieldDescriptor<sdk.PaymentTransaction>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.PaymentTransaction> item = new SortFieldDescriptor<sdk.PaymentTransaction>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.PaymentTransaction> defaultSort = new SortFieldDescriptor<sdk.PaymentTransaction>()
                    .Field(r => r.paymenttransaction_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.PaymentTransaction> searchResponse = client.Search<sdk.PaymentTransaction>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.PaymentTransaction> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.PaymentTransaction> GetByPaymentId(Guid payment_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByPaymentId", delegate ()
            {
                QueryContainer query = Query<sdk.PaymentTransaction>.Term(w => w.payment_id, payment_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.PaymentTransaction>> sortFields = new List<SortFieldDescriptor<sdk.PaymentTransaction>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.PaymentTransaction> item = new SortFieldDescriptor<sdk.PaymentTransaction>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.PaymentTransaction> defaultSort = new SortFieldDescriptor<sdk.PaymentTransaction>()
                    .Field(r => r.paymenttransaction_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.PaymentTransaction> searchResponse = client.Search<sdk.PaymentTransaction>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.PaymentTransaction> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
