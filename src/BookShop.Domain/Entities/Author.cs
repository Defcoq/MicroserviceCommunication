using System;
using System.Collections.Generic;

namespace BookShop.Domain.Entities
{
    public class Author
    {
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}