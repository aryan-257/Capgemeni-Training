using FluentValidation;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1000, 2024);
        }
    }
}
