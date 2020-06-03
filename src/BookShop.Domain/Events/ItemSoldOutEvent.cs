using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Events
{
    public class ItemSoldOutEvent  
    {
        public string Id { get; set; }

      
    }
}
