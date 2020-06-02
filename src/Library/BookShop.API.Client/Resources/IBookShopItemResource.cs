using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Contract.Item;

namespace BookShop.API.Client.Resources
{
    public interface IBookShopItemResource
    {
        Task<ItemResponse> Get(Guid id, CancellationToken cancellationToken = default);
    }
}