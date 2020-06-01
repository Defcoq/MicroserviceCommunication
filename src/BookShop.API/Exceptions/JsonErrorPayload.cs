using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.API.Exceptions
{
    public class JsonErrorPayload
    {
        public int EventId { get; set; }
        public object DetailedMessage { get; set; }
    }
}
