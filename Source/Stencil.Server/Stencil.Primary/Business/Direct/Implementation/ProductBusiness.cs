﻿using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Domain;
using Stencil.Data.Sql;
using Stencil.Primary.Synchronization;


namespace Stencil.Primary.Business.Direct.Implementation
{
    partial class ProductBusiness
    {
        partial void AfterInsertIndexed(StencilContext db, dbProduct product)
        {
            this.API.Direct.Brands.Invalidate(product.brand_id, "new product");
        }
    }
}