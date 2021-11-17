using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class Shipment : DomainModel
    {	
        public Shipment()
        {
				
        }
    
        public Guid shipment_id { get; set; }
        public Guid order_id { get; set; }
        public CarrierType shipment_carriertype { get; set; }
        public bool shipment_processed_successful { get; set; }
        public string shipment_street { get; set; }
        public string shipment_city { get; set; }
        public string shipment_state { get; set; }
        public string shipment_country { get; set; }
        public int shipment_zip { get; set; }
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

