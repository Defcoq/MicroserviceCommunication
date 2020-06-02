using System;

namespace BookShop.API.Contract.Item
{
    public class AuthorResponse
    {
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}