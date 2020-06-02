using BookShop.API.Client.Resources;

namespace BookShop.API.Client
{
    public interface IBookShopClient
    {
        IBookShopItemResource Item { get; }
    }
}