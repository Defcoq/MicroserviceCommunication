using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookCart.Services.Events
{
    public class BookDeleteIntegrationEvent : IntegrationEvent
    {
        public string BookId { get; set; }

        public BookDeleteIntegrationEvent(string bookId)
        {
            BookId = bookId;
        }
    }
}
