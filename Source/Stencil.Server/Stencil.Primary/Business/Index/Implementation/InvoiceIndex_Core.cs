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
    public partial class InvoiceIndex : IndexerBase<sdk.Invoice>, IInvoiceIndex
    {
        public InvoiceIndex(IFoundation foundation)
            : base(foundation, "InvoiceIndex", DocumentNames.Invoice)
        {

        }
        protected override string GetModelId(sdk.Invoice model)
        {
            return model.invoice_id.ToString();
        }
        public ListResult<sdk.Invoice> GetByOrderId(Guid order_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByOrderId", delegate ()
            {
                QueryContainer query = Query<sdk.Invoice>.Term(w => w.order_id, order_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Invoice>> sortFields = new List<SortFieldDescriptor<sdk.Invoice>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Invoice> item = new SortFieldDescriptor<sdk.Invoice>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Invoice> defaultSort = new SortFieldDescriptor<sdk.Invoice>()
                    .Field(r => r.invoice_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Invoice> searchResponse = client.Search<sdk.Invoice>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Invoice> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        public ListResult<sdk.Invoice> GetByAssetId(Guid asset_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByAssetId", delegate ()
            {
                QueryContainer query = Query<sdk.Invoice>.Term(w => w.asset_id, asset_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Invoice>> sortFields = new List<SortFieldDescriptor<sdk.Invoice>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Invoice> item = new SortFieldDescriptor<sdk.Invoice>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Invoice> defaultSort = new SortFieldDescriptor<sdk.Invoice>()
                    .Field(r => r.invoice_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Invoice> searchResponse = client.Search<sdk.Invoice>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Invoice> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
