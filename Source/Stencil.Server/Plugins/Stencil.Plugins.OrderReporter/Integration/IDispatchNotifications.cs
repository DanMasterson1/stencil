using Stencil.Plugins.OrderInformant.Models;

namespace Stencil.Plugins.OrderInformant.Integration
{
    public interface IDispatchNotifications
    {
        void Dispatch(OrderNotification notification);
    }
}