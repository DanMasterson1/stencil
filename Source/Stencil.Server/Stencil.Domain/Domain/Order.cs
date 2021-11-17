using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class Order : DomainModel
    {	
        public Order()
        {
				
        }
    
        public Guid order_id { get; set; }
        public Guid account_id { get; set; }
        public Guid? invoice_id { get; set; }
        public Guid? payment_id { get; set; }
        public Guid? shipment_id { get; set; }
        public bool order_paid { get; set; }
        public bool order_shipped { get; set; }
        public OrderStatus order_status { get; set; }
        public DateTime created_utc { get; set; }
        public DateTime updated_utc { get; set; }
        public DateTime? deleted_utc { get; set; }
        public DateTime? sync_success_utc { get; set; }
        public DateTime? sync_invalid_utc { get; set; }
        public DateTime? sync_attempt_utc { get; set; }
        public string sync_agent { get; set; }
        public string sync_log { get; set; }
	}
}

