using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Models
{
    public class QueryReportRequest
    {
        public QueryReportRequest()
        {

        }
        public string entity { get; set; }

        public string query_context { get; set; }

        public string response { get; set; }
    }
}
