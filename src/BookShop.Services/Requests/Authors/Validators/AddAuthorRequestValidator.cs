using BookShop.Services.Requests.Authors;
using FluentValidation;

namespace BookShop.Services.Requests.Authors.Validators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(artist => artist.AuthorName).NotEmpty();
        }
    }
}