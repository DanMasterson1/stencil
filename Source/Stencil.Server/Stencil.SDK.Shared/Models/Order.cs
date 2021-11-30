using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Order : SDKModel
    {	
        public Order()
        {
				
        }
    
        public virtual Guid order_id { get; set; }
        public virtual Guid account_id { get; set; }
        public virtual Guid? invoice_id { get; set; }
        public virtual Guid? payment_id { get; set; }
        public virtual Guid? shipment_id { get; set; }
        public virtual bool order_paid { get; set; }
        public virtual bool order_shipped { get; set; }
        public virtual OrderStatus order_status { get; set; }
        
        //<IndexOnly>
        
        public decimal order_total { get; set; }
        public DateTime created_utc { get; set; }
        public string account_name { get; set; }
        public string account_email { get; set; }
        public string shipment_address { get; set; }
        public string payment_cardtype { get; set; }
        public string status { get; set; }
        public int lineitem_count { get; set; }
        public Product[] products { get; set; }
        
        //</IndexOnly>
	}
}

