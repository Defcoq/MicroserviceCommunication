using BookShop.API.ResponseModels;
using BookShop.Domain.Entities;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using BookShopFixtures;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookShop.API.Tests
{
    public class BookControllerTests: IClassFixture<InMemoryApplicationFactory<Startup>>
    {
        public BookControllerTests(InMemoryApplicationFactory<Startup> factory)
        {
            _factory = new InMemoryApplicationFactory<Startup>();
        }

        private readonly InMemoryApplicationFactory<Startup> _factory;

        [Theory]
        [InlineData("/api/books/?pageSize=1&pageIndex=0", 1, 0)]
        [InlineData("/api/books/?pageSize=2&pageIndex=0", 2, 0)]
        [InlineData("/api/books/?pageSize=1&pageIndex=1", 1, 1)]
        public async Task get_should_return_paginated_data(string url, int pageSize, int pageIndex)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity =
                JsonConvert.DeserializeObject<PaginatedBooksResponseModel<BookResponse>>(responseContent);

            responseEntity.PageIndex.ShouldBe(pageIndex);
            responseEntity.PageSize.ShouldBe(pageSize);
            responseEntity.Data.Count().ShouldBe(pageSize);
        }

        [Theory]
        [LoadData("book")]
        public async Task get_by_id_should_return_the_data(Book request)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/books/{request.Id}");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<BookResponse>(responseContent);

            responseEntity.Name.ShouldBe(request.Name);
            responseEntity.Description.ShouldBe(request.Description);
            responseEntity.Price.Amount.ShouldBe(request.Price.Amount);
            responseEntity.Price.Currency.ShouldBe(request.Price.Currency);
            responseEntity.Format.ShouldBe(request.Format);
            responseEntity.PictureUri.ShouldBe(request.PictureUri);
            responseEntity.GenreId.ShouldBe(request.GenreId);
            responseEntity.AuthorId.ShouldBe(request.AuthorId);
        }

        [Theory]
        [LoadData("book")]
        public async Task add_should_create_new_record(AddBookRequest request)
        {
            var client = _factory.CreateClient();

            var httpContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/books", httpContent);

            response.EnsureSuccessStatusCode();
            response.Headers.Location.ShouldNotBeNull();
        }

        [Theory]
        [LoadData("book")]
        public async Task add_should_returns_bad_request_if_artistid_not_exist(AddBookRequest request)
        {
            var client = _factory.CreateClient();

            request.AuthorId = Guid.NewGuid();
            var httpContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/books", httpContent);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Theory]
        [LoadData("book")]
        public async Task add_should_returns_bad_request_if_genreid_not_exist(AddBookRequest request)
        {
            var client = _factory.CreateClient();

            request.GenreId = Guid.NewGuid();
            var httpContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/books", httpContent);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Theory]
        [LoadData("book")]
        public async Task update_should_modify_existing_books(EditBookRequest request)
        {
            var client = _factory.CreateClient();

            var httpContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/books/{request.Id}", httpContent);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<BookResponse>(responseContent);

            responseEntity.Name.ShouldBe(request.Name);
            responseEntity.Description.ShouldBe(request.Description);
            responseEntity.Format.ShouldBe(request.Format);
            responseEntity.PictureUri.ShouldBe(request.PictureUri);
            responseEntity.GenreId.ShouldBe(request.GenreId);
            responseEntity.AuthorId.ShouldBe(request.AuthorId);
        }

        [Theory]
        [LoadData("book")]
        public async Task update_should_returns_not_found_when_book_is_not_present(EditBookRequest request)
        {
            var client = _factory.CreateClient();

            var httpContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/books/{Guid.NewGuid()}", httpContent);

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }


        [Theory]
        [LoadData("book")]
        public async Task update_should_returns_bad_request_if_artistid_not_exist(EditBookRequest request)
        {
            var client = _factory.CreateClient();

            request.AuthorId = Guid.NewGuid();
            var httpContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/books/{request.Id}", httpContent);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Theory]
        [LoadData("book")]
        public async Task update_should_returns_bad_request_if_genreid_not_exist(EditBookRequest request)
        {
            var client = _factory.CreateClient();

            request.GenreId = Guid.NewGuid();
            var httpContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/books/{request.Id}", httpContent);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Theory]
        [LoadData("book")]
        public async Task delete_should_returns_no_content_when_called_with_right_id(DeleteBookRequest request)
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync($"/api/books/{request.Id}");
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task delete_should_returns_not_found_when_called_with_not_existing_id()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/books/{Guid.NewGuid()}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task getbyid_should_returns_not_found_when_book_is_not_present()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/books/{Guid.NewGuid()}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
