using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class EntitySubscription : DomainModel
    {	
        public EntitySubscription()
        {
				
        }
    
        public Guid subscription_id { get; set; }
        public Guid brand_id { get; set; }
        public string event { get; set; }
        public string url { get; set; }
        
	}
}

