using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Listing : SDKModel
    {	
        public Listing()
        {
				
        }
    
        public virtual Guid listing_id { get; set; }
        public virtual Guid brand_id { get; set; }
        public virtual Guid product_id { get; set; }
        public virtual Guid? promotion_id { get; set; }
        public virtual string listing_description { get; set; }
        public virtual bool active { get; set; }
        public virtual DateTime expire_utc { get; set; }
        
        //<IndexOnly>
        
        public decimal listing_price { get; set; }
        public decimal product_baseprice { get; set; }
        public decimal? promotion_percent { get; set; }
        public string brand_name { get; set; }
        public string product_name { get; set; }
        public string promotion_description { get; set; }
        
        //</IndexOnly>
	}
}

