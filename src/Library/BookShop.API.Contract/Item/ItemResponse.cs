using System;

namespace BookShop.API.Contract.Item
{
    public class ItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LabelName { get; set; }
        public MoneyResponse Price { get; set; }
        public string PictureUri { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public string Format { get; set; }
        public int AvailableStock { get; set; }
        public GenreResponse Genre { get; set; }
        public AuthorResponse Author { get; set; }
    }
}