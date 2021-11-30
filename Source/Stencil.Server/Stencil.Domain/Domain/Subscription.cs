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
        public string event_name { get; set; }
        public string url { get; set; }
        
	}
}

