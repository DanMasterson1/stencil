using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class EntitySubscription : SDKModel
    {	
        public EntitySubscription()
        {
				
        }
    
        public virtual Guid subscription_id { get; set; }
        public virtual Guid brand_id { get; set; }
        public virtual string event { get; set; }
        public virtual string url { get; set; }
        
	}
}

