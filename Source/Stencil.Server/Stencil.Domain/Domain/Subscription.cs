using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class Subscription : DomainModel
    {	
        public Subscription()
        {
				
        }
    
        public Guid subscription_id { get; set; }
        public Guid brand_id { get; set; }
        public Guid product_id { get; set; }
        public DateTime timestamp { get; set; }
        
	}
}

