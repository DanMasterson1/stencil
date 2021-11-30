using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Models
{
    public class NotifyPluginRequest
    {
        public NotifyPluginRequest()
        {

        }
        public string eventName { get; set; }

        public string query_context { get; set; }

        public string response { get; set; }

        public Guid brand_id { get; set; }
    }
}
