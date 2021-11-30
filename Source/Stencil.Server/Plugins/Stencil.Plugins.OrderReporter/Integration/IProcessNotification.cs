using Stencil.Plugins.OrderInformant.Models;

namespace Stencil.Plugins.OrderInformant.Integration
{
    public interface IProcessNotification
    {
        void Send(OrderNotification notification);
    }
}