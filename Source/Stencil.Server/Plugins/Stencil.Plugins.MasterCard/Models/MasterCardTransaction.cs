using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Stencil.Plugins.MasterCard.Models
{
    public class MasterCardTransaction
    {
        public SourceOfFunds source { get; set; }

        public Guid order_id { get; set; }
     
        public double order_amount { get; set; }
      
        public string order_currency { get; set; }
      
        public Guid transaction_id { get; set; }

    }
}