using FluentValidation;

namespace BookShop.Services.Requests.Genre.Validators
{
    public class AddGenreRequestValidator : AbstractValidator<AddGenreRequest>
    {
        public AddGenreRequestValidator()
        {
            RuleFor(genre => genre.GenreDescription).NotEmpty();
        }
    }
}