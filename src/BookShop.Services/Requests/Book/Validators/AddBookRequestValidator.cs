using BookShop.Services.Contracts;
using BookShop.Services.Requests.Authors;
using BookShop.Services.Requests.Genre;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Services.Requests.Book.Validators
{
        
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        public AddBookRequestValidator(IAuthorService authorService, IGenreService genreService)
        {
            _authorService = authorService;
            _genreService = genreService;
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .MustAsync(AuthorExists).WithMessage("Author must exists");
            RuleFor(x => x.GenreId)
                .NotEmpty()
                .MustAsync(GenreExists).WithMessage("Genre must exists");
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.ReleaseDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).Must(x => x?.Amount > 0);
            RuleFor(x => x.AvailableStock).Must(x => x > 0);
        }

        private async Task<bool> AuthorExists(Guid artistId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(artistId.ToString())) return false;

            var artist = await _authorService.GetAuthorAsync(new GetAuthorRequest { Id = artistId });
            return artist != null;
        }

        private async Task<bool> GenreExists(Guid genreId, CancellationToken token)
        {
            if (string.IsNullOrEmpty(genreId.ToString())) return false;

            var genre = await _genreService.GetGenreAsync(new GetGenreRequest { Id = genreId });
            return genre != null;
        }
    }
}
