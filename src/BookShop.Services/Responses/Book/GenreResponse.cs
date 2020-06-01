using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Responses.Book
{
   public  class GenreResponse
    {
        public Guid GenreId { get; set; }
        public string GenreDescription { get; set; }
    }
}
