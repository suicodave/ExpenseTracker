using FluentValidation;

using Shared.Auth;

namespace Shared.Auth
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(x=>x.Email)
            .EmailAddress()
            .NotEmpty();
            
            RuleFor(x=>x.Password)
            .NotEmpty();
        }
    }
}