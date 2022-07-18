using FluentValidation;

namespace Shared.AccountTypes
{
    public class AccountTypeRequestValidator:AbstractValidator<AccountTypeRequest>
    {
        public AccountTypeRequestValidator()
        {
            RuleFor(x=>x.Name)
            .NotEmpty();
        }
    }
}