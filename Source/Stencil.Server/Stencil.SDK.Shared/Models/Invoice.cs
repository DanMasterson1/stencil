using System;
using System.Collections.Generic;
using System.Text;

namespace Stencil.SDK.Models
{
    public partial class Invoice : SDKModel
    {	
        public Invoice()
        {
				
        }
    
        public virtual Guid invoice_id { get; set; }
        public virtual Guid order_id { get; set; }
        public virtual Guid asset_id { get; set; }
        
	}
}

