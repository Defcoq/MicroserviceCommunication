using BookShop.Domain.Entities;
using BookShop.Domain.Repositories;
using BookShop.Services.Contracts;
using BookShop.Services.Mappers;
using BookShop.Services.Requests.Genre;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreMapper _genreMapper;
        private readonly IGenreRepository _genreRepository;
        private readonly IBookMapper _itemMapper;
        private readonly IBookRepository _itemRepository;

        public GenreService(IGenreRepository genreRepository, IBookRepository itemRepository,
            IGenreMapper genreMapper, IBookMapper itemMapper)
        {
            _genreRepository = genreRepository;
            _itemRepository = itemRepository;
            _genreMapper = genreMapper;
            _itemMapper = itemMapper;
        }

        public async Task<IEnumerable<GenreResponse>> GetGenreAsync()
        {
            var result = await _genreRepository.GetAsync();
            return result.Select(_genreMapper.Map);
        }

        public async Task<GenreResponse> GetGenreAsync(GetGenreRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();

            var result = await _genreRepository.GetAsync(request.Id);
            return result == null ? null : _genreMapper.Map(result);
        }

        public async Task<IEnumerable<BookResponse>> GetBookByGenreIdAsync(GetGenreRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var result = await _itemRepository.GetBookByGenreIdAsync(request.Id);
            return result.Select(_itemMapper.Map);
        }

        public async Task<GenreResponse> AddGenreAsync(AddGenreRequest request)
        {
            var item = new Genre { GenreDescription = request.GenreDescription };

            var result = _genreRepository.Add(item);
            await _genreRepository.UnitOfWork.SaveChangesAsync();

            return _genreMapper.Map(result);
        }

        
    }
}
