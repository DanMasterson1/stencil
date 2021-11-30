using Stencil.Plugins.ProductInformant.Models;

namespace Stencil.Plugins.ProductInformant
{
    public interface IProcessNotification
    {
        void Send(ProductNotification notification);
    }
}