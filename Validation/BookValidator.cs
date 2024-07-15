using FluentValidation;
using PaparaPatika.ViewModels;

namespace PaparaPatika.Validation
{
    public class BookValidator : AbstractValidator<BookViewModel>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.").MinimumLength(1).WithMessage("Title must be minimum 1 character").MaximumLength(100).WithMessage("Title must be maximum 100 characters.");
        }
    }
}
