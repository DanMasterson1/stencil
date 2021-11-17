using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class Payment : DomainModel
    {	
        public Payment()
        {
				
        }
    
        public Guid payment_id { get; set; }
        public Guid order_id { get; set; }
        public bool payment_processed_successful { get; set; }
        public CardType card_type { get; set; }
        public string card_number { get; set; }
        public DateTime expire_date { get; set; }
        public int cvv { get; set; }
        public bool save_paymentdetails { get; set; }
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

