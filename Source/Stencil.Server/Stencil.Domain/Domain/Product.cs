using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class Product : DomainModel
    {	
        public Product()
        {
				
        }
    
        public Guid product_id { get; set; }
        public Guid brand_id { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public decimal baseprice { get; set; }
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
