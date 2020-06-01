using BookShop.API.Filters;
using BookShop.Services.Contracts;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookShop.API.Tests.Filters
{
    public class BookExistsAttributeTests
    {
        [Fact]
        public async Task should_continue_pipeline_when_id_is_present()
        {
            var id = Guid.NewGuid();
            var itemService = new Mock<IBookService>();
            itemService
            .Setup(x => x.GetBookAsync(It.IsAny<GetBookRequest>()))
            .ReturnsAsync(new BookResponse { Id = id });
            var filter = new BookExistsAttribute.BookExistsFilterImpl(itemService.Object);
            var actionExecutedContext = new ActionExecutingContext(
            new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object>
            {
              {"id", id}
            }, new object());
            var nextCallback = new Mock<ActionExecutionDelegate>();
            await filter.OnActionExecutionAsync(actionExecutedContext,
            nextCallback.Object);
            nextCallback.Verify(executionDelegate => executionDelegate(), Times.Once);
        }
    }

}
