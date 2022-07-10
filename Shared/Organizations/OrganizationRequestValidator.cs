using FluentValidation;

namespace Shared.Organizations
{
    public class OrganizationRequestValidator : AbstractValidator<OrganizationRequest>
    {
        public OrganizationRequestValidator()
        {
            RuleFor(x=>x.Name)
            .NotEmpty();
        }
    }
}