using BookShop.Domain.Entities;
using BookShop.EFRepository.Repositories;
using BookShopFixtures;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookShop.EFRepository.Tests
{
    public class AuthorRepositoryTests : IClassFixture<BookShopContextFactory>
    {
        private readonly BookShopContextFactory _factory;

        public AuthorRepositoryTests(BookShopContextFactory factory)
        {
            _factory = factory;
        }
        [Theory]
        [LoadData("artist")]
        public async Task should_return_record_by_id(Author artist)
        {
            var sut = new AuthorRepository(_factory.ContextInstance);
            var result = await sut.GetAsync(artist.AuthorId);
            result.AuthorId.ShouldBe(artist.AuthorId);
            result.AuthorName.ShouldBe(artist.AuthorName);
        }
        [Theory]
        [LoadData("artist")]
        public async Task should_add_new_item(Author artist)
        {
            artist.AuthorId = Guid.NewGuid();
            var sut = new AuthorRepository(_factory.ContextInstance);
            sut.Add(artist);
            await sut.UnitOfWork.SaveEntitiesAsync();
            _factory.ContextInstance.Authors
            .FirstOrDefault(x => x.AuthorId == artist.AuthorId)
            .ShouldNotBeNull();
        }
        }
}
