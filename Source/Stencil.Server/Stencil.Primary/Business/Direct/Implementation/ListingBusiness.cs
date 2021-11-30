using Stencil.Data.Sql;
using Stencil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Business.Direct.Implementation
{
    public partial class ListingBusiness
    {
        public void InvalidateExpiredListings()
        {
            base.ExecuteMethod(nameof(InvalidateExpiredListings), delegate ()
            {
                using (StencilContext db = base.CreateSQLContext())
                {
                    

                    IQueryable<dbListing> result = (from n in db.dbListings
                                                    where n.active == true && n.expire_utc > DateTime.Now
                                                    select n);

                    foreach(dbListing listing in result)
                    {
                        Listing domainListing = listing.ToDomainModel();
                        domainListing.active = false;
                        domainListing.sync_success_utc = null;

                        this.API.Direct.Listings.Update(domainListing);
                    }

                }
            });
        }
    }
}
