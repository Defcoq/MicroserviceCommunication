using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.API.Filters;
using BookShop.API.ResponseModels;
using BookShop.Services.Contracts;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageSize = 10, int pageIndex = 0)
        {
            var result = await _bookService.GetBooksAsync();
            var totalItems = result.Count();
            var itemsOnPage = result
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize);
            var model = new PaginatedBooksResponseModel<BookResponse>(
            pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }
        [HttpGet("{id:guid}")]
        [BookExists]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _bookService.GetBookAsync(new GetBookRequest
            { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddBookRequest request)
        {
            var result = await _bookService.AddBookAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
            null);
        }
        [HttpPut("{id:guid}")]
        [BookExists]
        public async Task<IActionResult> Put(Guid id, EditBookRequest
        request)
        {
            request.Id = id;
            var result = await _bookService.EditBookAsync(request);
            return Ok(result);
        }
    }
}