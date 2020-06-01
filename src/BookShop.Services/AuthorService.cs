using BookShop.Domain.Repositories;
using BookShop.Services.Contracts;
using BookShop.Services.Mappers;
using BookShop.Services.Requests.Authors;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _artistRepository;
        private readonly IBookRepository _itemRepository;
        private readonly IAuthorMapper _artistMapper;
        private readonly IBookMapper _itemMapper;
        public AuthorService(IAuthorRepository artistRepository,
        IBookRepository itemRepository,
        IAuthorMapper artistMapper, IBookMapper itemMapper)
        {
            _artistRepository = artistRepository;
            _itemRepository = itemRepository;
            _artistMapper = artistMapper;
            _itemMapper = itemMapper;
        }

        public async Task<IEnumerable<AuthorResponse>> GetAuthorsAsync()
        {
            var result = await _artistRepository.GetAsync();
            return result.Select(_artistMapper.Map);
        }
        public async Task<AuthorResponse> GetAuthorAsync(GetAuthorRequest
request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var result = await _artistRepository.GetAsync(request.Id);
            return result == null ? null : _artistMapper.Map(result);
        }
        public async Task<IEnumerable<BookResponse>>
        GetBookByAuthorIdAsync(GetAuthorRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var result = await
            _itemRepository.GetBookByAuthorIdAsync(request.Id);
            return result.Select(_itemMapper.Map);
        }
        public async Task<AuthorResponse> AddAuthorAsync(AddAuthorRequest
        request)
        {
            var item = new Domain.Entities.Author
            {
                AuthorName =
            request.AuthorName
            };
            var result = _artistRepository.Add(item);
            await _artistRepository.UnitOfWork.SaveChangesAsync();
            return _artistMapper.Map(result);
        }
    }
}
