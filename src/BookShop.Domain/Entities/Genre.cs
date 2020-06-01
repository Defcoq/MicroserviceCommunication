using System;
using System.Collections.Generic;

namespace BookShop.Domain.Entities
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string GenreDescription { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}