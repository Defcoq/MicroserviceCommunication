using System.Threading;
using System.Threading.Tasks;
using Cart.Domain.Responses.Cart;

namespace Cart.Domain.Services
{
    public interface IBookShopService
    {
        Task<CartItemResponse> EnrichCartItem(CartItemResponse item, CancellationToken cancellationToken);
    }
}