using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Subscription : SDKModel
    {	
        public Subscription()
        {
				
        }
    
        public virtual Guid subscription_id { get; set; }
        public virtual Guid brand_id { get; set; }
        public virtual Guid product_id { get; set; }
        public virtual DateTime timestamp { get; set; }
        
	}
}

