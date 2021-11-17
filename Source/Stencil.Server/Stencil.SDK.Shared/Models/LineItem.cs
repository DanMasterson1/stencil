using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class LineItem : SDKModel
    {	
        public LineItem()
        {
				
        }
    
        public virtual Guid lineitem_id { get; set; }
        public virtual Guid order_id { get; set; }
        public virtual Guid listing_id { get; set; }
        public virtual int lineitem_quantity { get; set; }
        
        //<IndexOnly>
        
        public decimal listing_price { get; set; }
        public decimal lineitem_total { get; set; }
        
        //</IndexOnly>
	}
}

