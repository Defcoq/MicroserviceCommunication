using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Client.Base;
using BookShop.API.Contract.Item;

namespace BookShop.API.Client.Resources
{
    public class BookShopItemResource : IBookShopItemResource
    {
        private readonly IBaseClient _client;

        public BookShopItemResource(IBaseClient client)
        {
            _client = client;
        }

        public async Task<ItemResponse> Get(Guid id, CancellationToken cancellationToken)
        {
            var uri = BuildUri(id);
            return await _client.GetAsync<ItemResponse>(uri, cancellationToken);
        }

        private Uri BuildUri(Guid id, string path = "")
        {
            return _client.BuildUri(string.Format("api/books/{0}", id, path));
        }
    }
}