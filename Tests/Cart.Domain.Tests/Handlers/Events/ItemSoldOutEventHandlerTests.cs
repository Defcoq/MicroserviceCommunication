using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cart.Domain.Events;
using Cart.Domain.Handlers.Cart;
using Cart.Fixtures;
using Shouldly;
using Xunit;

namespace Cart.Domain.Tests.Handlers.Events
{
    public class ItemSoldOutEventHandlerTests : IClassFixture<CartContextFactory>
    {
        private readonly CartContextFactory _contextFactory;

        public ItemSoldOutEventHandlerTests(CartContextFactory cartContextFactory)
        {
            _contextFactory = cartContextFactory;
        }

        [Fact]
        public async Task should_not_remove_records_when_soldout_message_contains_not_existing_id()
        {
            var repository = _contextFactory.GetCartRepository();
            var itemSoldOutEventHandler = new ItemSoldOutEventHandler(repository);
            var found = false;

            await itemSoldOutEventHandler.Handle(new ItemSoldOutEvent { Id = Guid.NewGuid().ToString() }, CancellationToken.None);

            var cartsIds = repository.GetCarts();

            foreach (var cartId in cartsIds)
            {
                var cart = await repository.GetAsync(new Guid(cartId));
                found = cart.Items.Any(i => i.CartItemId.ToString() == "be05537d-5e80-45c1-bd8c-aa21c0f1251e");
            }

            found.ShouldBeTrue();
        }

        [Fact]
        public async Task should_remove_records_when_soldout_messages_contains_existing_ids()
        {
            var itemSoldOutId = "be05537d-5e80-45c1-bd8c-aa21c0f1251e";
            var repository = _contextFactory.GetCartRepository();
            var itemSoldOutEventHandler = new ItemSoldOutEventHandler(repository);
            var found = false;


            await itemSoldOutEventHandler.Handle(new ItemSoldOutEvent { Id = itemSoldOutId }, CancellationToken.None);

            foreach (var cartId in repository.GetCarts())
            {
                var cart = await repository.GetAsync(new Guid(cartId));
                found = cart.Items.Any(i => i.CartItemId.ToString() == itemSoldOutId);
            }

            found.ShouldBeFalse();
        }
    }
}