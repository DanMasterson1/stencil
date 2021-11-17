using Stencil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Workers.Models
{
    public class PaymentDetailRequest
    {
        public PaymentDetailRequest()
        {

        }

        public Guid account_id { get; set; }
        public CardType card_type { get; set; }
        public string card_number { get; set; }
        public DateTime expire_date { get; set; }
        public int cvv { get; set; }
    }
}
