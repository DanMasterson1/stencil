using Stencil.Primary.Integration;
using System.Collections.Generic;

namespace Stencil.Primary.Workers
{
    public interface IQueryReportWorker
    {
        StencilAPI API { get; set; }
        List<IProductNotify> Subscribers { get; }
    }
}