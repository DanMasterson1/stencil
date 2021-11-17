using Stencil.Primary.Workers;

namespace Stencil.Primary.Integration
{
    public interface IProductSubscription
    {
        IQueryReportWorker QueryReportWorker { get; }

        void AddSubscriber(IProductNotify subscriber);
    }
}