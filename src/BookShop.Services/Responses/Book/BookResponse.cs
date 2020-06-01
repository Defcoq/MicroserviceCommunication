using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Responses.Book
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LabelName { get; set; }
        public PriceResponse Price { get; set; }
        public string PictureUri { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }
        public string Format { get; set; }
        public int AvailableStock { get; set; }
        public Guid GenreId { get; set; }
        public GenreResponse Genre { get; set; }
        public Guid AuthorId { get; set; }
        public AuthorResponse Author { get; set; }
    }
}
