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
    public partial class ShipmentIndex : IndexerBase<sdk.Shipment>, IShipmentIndex
    {
        public ShipmentIndex(IFoundation foundation)
            : base(foundation, "ShipmentIndex", DocumentNames.Shipment)
        {

        }
        protected override string GetModelId(sdk.Shipment model)
        {
            return model.shipment_id.ToString();
        }
        public ListResult<sdk.Shipment> GetByOrderId(Guid order_id, int skip, int take, string order_by = "", bool descending = false)
        {
            return base.ExecuteFunction("GetByOrderId", delegate ()
            {
                QueryContainer query = Query<sdk.Shipment>.Term(w => w.order_id, order_id);

                

                int takePlus = take;
                if(take != int.MaxValue)
                {
                    takePlus++; // for stepping
                }
                
                List<SortFieldDescriptor<sdk.Shipment>> sortFields = new List<SortFieldDescriptor<sdk.Shipment>>();
                if(!string.IsNullOrEmpty(order_by))
                {
                    SortFieldDescriptor<sdk.Shipment> item = new SortFieldDescriptor<sdk.Shipment>()
                        .Field(order_by)
                        .Order(descending ? SortOrder.Descending : SortOrder.Ascending);
                        
                    sortFields.Add(item);
                }
                SortFieldDescriptor<sdk.Shipment> defaultSort = new SortFieldDescriptor<sdk.Shipment>()
                    .Field(r => r.shipment_id)
                    .Ascending();
                
                sortFields.Add(defaultSort);
                
                ElasticClient client = this.ClientFactory.CreateClient();
                ISearchResponse<sdk.Shipment> searchResponse = client.Search<sdk.Shipment>(s => s
                    .Query(q => query)
                    .Skip(skip)
                    .Take(takePlus)
                    .Sort(sr => sr.Multi(sortFields))
                    .Type(this.DocumentType));

                ListResult<sdk.Shipment> result = searchResponse.Documents.ToSteppedListResult(skip, take, searchResponse.GetTotalHit());
                
                return result;
            });
        }
        

    }
}
