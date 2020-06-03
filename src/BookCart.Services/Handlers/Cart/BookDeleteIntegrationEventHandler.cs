using BookCart.Services.Events;
using Cart.Domain.Events;
using Cart.Domain.Repositories;
using MediatR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCart.Services.Handlers.Cart
{
    public class BookDeleteIntegrationEventHandler : IIntegrationEventHandler<BookDeleteIntegrationEvent>
    {
        private readonly ILogger<BookDeleteIntegrationEventHandler> _logger;

        private readonly IMediator _mediator;

        public BookDeleteIntegrationEventHandler(IMediator mediator,
            ILogger<BookDeleteIntegrationEventHandler> logger,
            ICartRepository cartRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
   
            _mediator = mediator;
        }

        public async Task Handle(BookDeleteIntegrationEvent @event)
        {
            ItemSoldOutEvent evt = new ItemSoldOutEvent() { Id = @event.BookId };
            _logger.LogInformation("send BookDeleteIntegrationEvent to MediaR =>" + @event.BookId);
          await   _mediator.Send(evt);
        }
    }
}
