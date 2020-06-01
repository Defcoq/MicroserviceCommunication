using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Requests.Book.Validators
{
    public class EditBookRequestValidator :
 AbstractValidator<EditBookRequest>
    {
        public EditBookRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.AuthorId).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Price).Must(x => x?.Amount > 0);
            RuleFor(x => x.ReleaseDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
