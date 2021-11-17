using Stencil.Plugins.MasterCard.Integration;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace Stencil.Plugins.MasterCard.Models
{
    public class SourceOfFunds
    {
        
        public IPaymentProvider provided { get; set; }
        
        public string type { get; set; }
    }
}