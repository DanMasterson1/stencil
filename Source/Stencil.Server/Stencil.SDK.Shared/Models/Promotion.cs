using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Promotion : SDKModel
    {	
        public Promotion()
        {
				
        }
    
        public virtual Guid promotion_id { get; set; }
        public virtual PromotionType promotion_type { get; set; }
        public virtual string promotion_description { get; set; }
        public virtual decimal percent { get; set; }
        
        //<IndexOnly>
        
        public string promotion_name { get; set; }
        
        //</IndexOnly>
	}
}

