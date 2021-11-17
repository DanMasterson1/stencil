using Codeable.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stencil.Domain
{
    public partial class PaymentTransaction : DomainModel
    {	
        public PaymentTransaction()
        {
				
        }
    
        public Guid paymenttransaction_id { get; set; }
        public Guid order_id { get; set; }
        public Guid payment_id { get; set; }
        public TransactionOutcome transaction_outcome { get; set; }
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

