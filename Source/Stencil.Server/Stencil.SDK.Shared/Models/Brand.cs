using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Brand : SDKModel
    {	
        public Brand()
        {
				
        }
    
        public virtual Guid brand_id { get; set; }
        public virtual string brand_name { get; set; }
        
        //<IndexOnly>
        
        public int product_count { get; set; }
        public int listing_count { get; set; }
        
        //</IndexOnly>
	}
}

