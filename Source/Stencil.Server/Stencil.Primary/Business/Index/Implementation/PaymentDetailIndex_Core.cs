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
    public partial class PaymentDetailIndex : IndexerBase<sdk.PaymentDetail>, IPaymentDetailIndex
    {
        public PaymentDetailIndex(IFoundation foundation)
            : base(foundation, "PaymentDetailIndex", DocumentNames.PaymentDetail)
        {

        }
        protected override string GetModelId(sdk.PaymentDetail model)
        {
            return model.paymentdetail_id.ToString();
        }
        public ListResult<sdk.PaymentDetail> GetByAccountId(Guid account_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByAccountId", delegate ()
            {
                QueryContainer query = Query<sdk.PaymentDetail>.Term(w => w.account_id, account_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.PaymentDetail>> sortFields = new List<SortFieldDescriptor<sdk.PaymentDetail>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.PaymentDetail> item = new SortFieldDescriptor<sdk.PaymentDetail>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.PaymentDetail> defaultSort = new SortFieldDescriptor<sdk.PaymentDetail>()
                    .Field(r => r.paymentdetail_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.PaymentDetail> searchResponse = client.Search<sdk.PaymentDetail>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.PaymentDetail> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
