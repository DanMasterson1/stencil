using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Direct
{
    partial interface IListingBusiness
    {
        void InvalidateExpiredListings();
    }
}
