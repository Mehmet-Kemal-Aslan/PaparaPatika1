using FluentValidation;
using PaparaPatika.Entitities;
using PaparaPatika.ViewModels;

namespace PaparaPatika.Validation
{
    public class AuthorValidator : AbstractValidator<AuthorViewModel>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").MinimumLength(2).WithMessage("Name must be minimum 1 character").MaximumLength(100).WithMessage("Name must be maximum 100 characters.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Surname is required.").MinimumLength(2).WithMessage("Surname must be minimum 1 character").MaximumLength(100).WithMessage("Surname must be maximum 100 characters.");
        }
    }
}
