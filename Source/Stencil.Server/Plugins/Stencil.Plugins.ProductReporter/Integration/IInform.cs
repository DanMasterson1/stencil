﻿using Stencil.Primary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Plugins.ProductInformant.Integration
{
    public interface IInform
    {
        void Inform(NotifyPluginRequest request);
    }
}
