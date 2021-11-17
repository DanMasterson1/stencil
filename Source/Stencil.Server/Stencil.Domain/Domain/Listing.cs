using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class Listing : DomainModel
    {	
        public Listing()
        {
				
        }
    
        public Guid listing_id { get; set; }
        public Guid brand_id { get; set; }
        public Guid product_id { get; set; }
        public Guid? promotion_id { get; set; }
        public string listing_description { get; set; }
        public bool active { get; set; }
        public DateTime expire_utc { get; set; }
        public DateTime created_utc { get; set; }
        public DateTime updated_utc { get; set; }
        public DateTime? deleted_utc { get; set; }
        public DateTime? sync_success_utc { get; set; }
        public DateTime? sync_invalid_utc { get; set; }
        public DateTime? sync_attempt_utc { get; set; }
        public string sync_agent { get; set; }
        public string sync_log { get; set; }
	}
}

