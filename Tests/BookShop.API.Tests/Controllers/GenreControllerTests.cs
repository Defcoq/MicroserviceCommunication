﻿using BookShop.API.ResponseModels;
using BookShop.Domain.Entities;
using BookShop.Services.Responses.Book;
using BookShopFixtures;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookShop.API.Tests.Controllers
{
    public class GenreControllerTests : IClassFixture<InMemoryApplicationFactory<Startup>>
    {
        public GenreControllerTests(InMemoryApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private readonly InMemoryApplicationFactory<Startup> _factory;

        [Theory]
        [InlineData("/api/genre/")]
        public async Task smoke_test_on_items(string url)

        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/api/genre/?pageSize=1&pageIndex=0", 1, 0)]
        public async Task get_should_returns_paginated_data(string url, int pageSize, int pageIndex)

        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity =
                JsonConvert.DeserializeObject<PaginatedBooksResponseModel<GenreResponse>>(responseContent);

            responseEntity.PageIndex.ShouldBe(pageIndex);
            responseEntity.PageSize.ShouldBe(pageSize);
            responseEntity.Data.Count().ShouldBe(pageSize);
        }

        [Theory]
        [LoadData("genre")]
        public async Task get_by_id_should_return_the_data(Genre request)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/genre/{request.GenreId}");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Genre>(responseContent);

            responseEntity.GenreId.ShouldBe(request.GenreId);
            responseEntity.GenreDescription.ShouldBe(request.GenreDescription);
        }

        [Theory]
        [LoadData("genre")]
        public async Task get_item_by_genre_should_return_the_data(Genre request)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/genre/{request.GenreId}/books");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<List<BookResponse>>(responseContent);

            responseEntity.Count.ShouldBe(3);
        }

        [Fact]
        public async Task add_should_create_new_genre()
        {
            var genreDescription = new { GenreDescription = "Jazz" };
            var client = _factory.CreateClient();

            var httpContent = new StringContent(JsonConvert.SerializeObject(genreDescription), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("/api/genre", httpContent);

            response.EnsureSuccessStatusCode();

            var responseHeader = response.Headers.Location;

            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            responseHeader.ToString().ShouldContain("/api/genre/");
        }
    }
}
