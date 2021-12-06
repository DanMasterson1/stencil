using Stencil.Plugins.ProductInformant.Models;

namespace Stencil.Plugins.ProductInformant
{
    public interface IDispatchNotifications
    {
        void Dispatch(ProductNotification notification);
    }
}