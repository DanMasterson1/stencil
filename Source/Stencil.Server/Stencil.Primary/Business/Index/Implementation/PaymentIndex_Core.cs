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
    public partial class PaymentIndex : IndexerBase<sdk.Payment>, IPaymentIndex
    {
        public PaymentIndex(IFoundation foundation)
            : base(foundation, "PaymentIndex", DocumentNames.Payment)
        {

        }
        protected override string GetModelId(sdk.Payment model)
        {
            return model.payment_id.ToString();
        }
        public ListResult<sdk.Payment> GetByOrderId(Guid order_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByOrderId", delegate ()
            {
                QueryContainer query = Query<sdk.Payment>.Term(w => w.order_id, order_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Payment>> sortFields = new List<SortFieldDescriptor<sdk.Payment>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Payment> item = new SortFieldDescriptor<sdk.Payment>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Payment> defaultSort = new SortFieldDescriptor<sdk.Payment>()
                    .Field(r => r.payment_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Payment> searchResponse = client.Search<sdk.Payment>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Payment> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
