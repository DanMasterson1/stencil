using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class PaymentDetail : SDKModel
    {	
        public PaymentDetail()
        {
				
        }
    
        public virtual Guid paymentdetail_id { get; set; }
        public virtual Guid account_id { get; set; }
        public virtual CardType card_type { get; set; }
        public virtual string card_number { get; set; }
        public virtual DateTime expire_date { get; set; }
        public virtual int cvv { get; set; }
        
	}
}

