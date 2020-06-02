using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cart.Domain.Commands.Cart;
using Cart.Domain.Repositories;
using Cart.Domain.Responses.Cart;
using Cart.Domain.Services;
using MediatR;

namespace Cart.Domain.Handlers.Cart
{
    public class UpdateCartItemQuantity : IRequestHandler<UpdateCartItemQuantityCommand, CartExtendedResponse>
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;
        private readonly ICartRepository _repository;

        public UpdateCartItemQuantity(ICartRepository repository, IMapper mapper, ICatalogService catalogService)
        {
            _repository = repository;
            _mapper = mapper;
            _catalogService = catalogService;
        }

        public async Task<CartExtendedResponse> Handle(UpdateCartItemQuantityCommand command,
            CancellationToken cancellationToken)
        {
            var cartDetail = await _repository.GetAsync(command.CartId);

            if (command.IsAddOperation)
                cartDetail.Items.FirstOrDefault(x => x.CartItemId == command.CartItemId)?.IncreaseQuantity();
            else
                cartDetail.Items.FirstOrDefault(x => x.CartItemId == command.CartItemId)?.DecreaseQuantity();

            var cartItemsList = cartDetail.Items.ToList();

            cartItemsList.RemoveAll(x => x.Quantity <= 0);

            cartDetail.Items = cartItemsList;

            await _repository.AddOrUpdateAsync(cartDetail);

            var response = _mapper.Map<CartExtendedResponse>(cartDetail);

            var tasks = response.Items
                .Select(async x => await _catalogService.EnrichCartItem(x, cancellationToken));

            response.Items = await Task.WhenAll(tasks);

            return response;
        }

        public CartExtendedResponse Handle(UpdateCartItemQuantityCommand message)
        {
            throw new NotImplementedException();
        }
    }
}