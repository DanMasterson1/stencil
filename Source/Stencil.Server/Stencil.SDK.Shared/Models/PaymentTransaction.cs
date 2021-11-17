using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class PaymentTransaction : SDKModel
    {	
        public PaymentTransaction()
        {
				
        }
    
        public virtual Guid paymenttransaction_id { get; set; }
        public virtual Guid order_id { get; set; }
        public virtual Guid payment_id { get; set; }
        public virtual TransactionOutcome transaction_outcome { get; set; }
        
	}
}

