using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Product : SDKModel
    {	
        public Product()
        {
				
        }
    
        public virtual Guid product_id { get; set; }
        public virtual Guid brand_id { get; set; }
        public virtual string product_name { get; set; }
        public virtual string product_description { get; set; }
        public virtual decimal baseprice { get; set; }
        
        //<IndexOnly>
        
        public string brand_name { get; set; }
        public bool promotional { get; set; }
        
        //</IndexOnly>
	}
}

