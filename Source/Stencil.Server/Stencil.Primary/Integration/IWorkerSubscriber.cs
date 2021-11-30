using Stencil.Primary.Models;

namespace Stencil.Primary.Integration
{
    public interface IWorkerSubscriber
    {
        string SubscribingEvent { get; set; }
        void NotifyPlugin(NotifyPluginRequest request);
    }
}
