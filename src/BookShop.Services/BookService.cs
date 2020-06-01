using BookShop.Domain.Repositories;
using BookShop.Services.Contracts;
using BookShop.Services.Mappers;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookMapper _bookMapper;

        public BookService(IBookRepository bookRepository, IBookMapper bookMapper)
        {
            _bookRepository = bookRepository;
            _bookMapper = bookMapper;
        }
        public async Task<BookResponse> AddBookAsync(AddBookRequest request)
        {
            var item = _bookMapper.Map(request);
            var result = _bookRepository.Add(item);
            await _bookRepository.UnitOfWork.SaveChangesAsync();
            return _bookMapper.Map(result);
        }

        public Task<BookResponse> DeleteBookAsync(DeleteBookRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<BookResponse> EditBookAsync(EditBookRequest request)
        {
            var existingRecord = await _bookRepository.GetAsync(request.Id);
            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.Id} is not present");
            }
            var entity = _bookMapper.Map(request);
            var result = _bookRepository.Update(entity);
            await _bookRepository.UnitOfWork.SaveChangesAsync();
            return _bookMapper.Map(result);
        }

        public async Task<BookResponse> GetBookAsync(GetBookRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var entity = await _bookRepository.GetAsync(request.Id);
            return _bookMapper.Map(entity);
        }

        public async Task<IEnumerable<BookResponse>> GetBooksAsync()
        {
            var result = await _bookRepository.GetAsync();
            return result.Select(x => _bookMapper.Map(x));
        }
    }
}
