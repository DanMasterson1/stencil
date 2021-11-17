using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Shipment : SDKModel
    {	
        public Shipment()
        {
				
        }
    
        public virtual Guid shipment_id { get; set; }
        public virtual Guid order_id { get; set; }
        public virtual CarrierType shipment_carriertype { get; set; }
        public virtual bool shipment_processed_successful { get; set; }
        public virtual string shipment_street { get; set; }
        public virtual string shipment_city { get; set; }
        public virtual string shipment_state { get; set; }
        public virtual string shipment_country { get; set; }
        public virtual int shipment_zip { get; set; }
        
	}
}

