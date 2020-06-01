using BookShop.Services.Contracts;
using BookShop.Services.Requests.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.API.Filters
{
    public class BookExistsAttribute : TypeFilterAttribute
    {
        public BookExistsAttribute() : base(typeof(BookExistsFilterImpl))
        {
        }

        public class BookExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IBookService _itemService;
            public BookExistsFilterImpl(IBookService itemService)
            {
                _itemService = itemService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext
            context,
            ActionExecutionDelegate next)
            {
                if (!(context.ActionArguments["id"] is Guid id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }
                var result = await _itemService.GetBookAsync(new GetBookRequest
                { Id = id });
                if (result == null)
                {
                    context.Result = new NotFoundObjectResult($"Item with id { id } not exist.");
                return;
                }
                await next();
            }
        }
    }
}
