using Stencil.Primary.Integration;
using System.Collections.Generic;

namespace Stencil.Primary.Workers
{
    public interface INotifyPlugin
    {
        StencilAPI API { get; set; }
        List<IWorkerSubscriber> WorkerSubscribers { get; }
    }
}