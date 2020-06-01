using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.API.Filters;
using BookShop.API.ResponseModels;
using BookShop.Services.Contracts;
using BookShop.Services.Requests.Authors;
using BookShop.Services.Responses.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers
{
    [Route("api/author")]
    [ApiController]
    [JsonException]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _artistService;

        public AuthorController(IAuthorService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _artistService.GetAuthorsAsync();

            var totalBooks = result.ToList().Count;

            var itemsOnPage = result
                .OrderBy(c => c.AuthorName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);

            var model = new PaginatedBooksResponseModel<AuthorResponse>(
                pageIndex, pageSize, totalBooks, itemsOnPage);

            return Ok(model);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _artistService.GetAuthorAsync(new GetAuthorRequest { Id = id });
            return Ok(result);
        }

        [HttpGet("{id:guid}/items")]
        public async Task<IActionResult> GetBooksById(Guid id)
        {
            var result = await _artistService.GetBookByAuthorIdAsync(new GetAuthorRequest { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddAuthorRequest request)
        {
            var result = await _artistService.AddAuthorAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.AuthorId }, null);
        }
    }
}