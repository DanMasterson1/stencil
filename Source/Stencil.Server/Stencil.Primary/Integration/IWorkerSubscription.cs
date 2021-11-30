using Stencil.Primary.Workers;

namespace Stencil.Primary.Integration
{
    public interface IWorkerSubscription
    {
        INotifyPlugin NotifyPluginWorker { get; }

        void AddSubscriber(IWorkerSubscriber subscriber);
    }
}