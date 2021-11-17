using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Payment : SDKModel
    {	
        public Payment()
        {
				
        }
    
        public virtual Guid payment_id { get; set; }
        public virtual Guid order_id { get; set; }
        public virtual bool payment_processed_successful { get; set; }
        public virtual CardType card_type { get; set; }
        public virtual string card_number { get; set; }
        public virtual DateTime expire_date { get; set; }
        public virtual int cvv { get; set; }
        public virtual bool save_paymentdetails { get; set; }
        
	}
}

