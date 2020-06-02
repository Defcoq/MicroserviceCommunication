using System.Net.Http;
using BookShop.API.Client.Base;
using BookShop.API.Client.Resources;

namespace BookShop.API.Client
{
    public class BookShopClient : IBookShopClient
    {
        public BookShopClient(HttpClient client)
        {
            Item = new BookShopItemResource(new BaseClient(client, client.BaseAddress.ToString()));
        }

        public IBookShopItemResource Item { get; }
    }
}