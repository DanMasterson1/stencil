using Stencil.Plugins.MasterCard.Integration;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace Stencil.Plugins.MasterCard.Models
{
    public class Card : IPaymentProvider
    {
       
        public string number { get; set; }
        
        public int expiryMonth { get; set; }
        
        public int expiryYear { get; set; }
        
        public int securityCode { get; set; }
    }
}