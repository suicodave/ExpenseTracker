using FluentValidation;

namespace Shared.Expenses
{
    public class ExpenseAccountRequestValidator : AbstractValidator<CreateExpenseAccountRequest>
    {
        public ExpenseAccountRequestValidator()
        {
            RuleFor(x=>x.Amount)
            .GreaterThan(0);

            RuleFor(x=>x.AccountTypeId)
            .NotEmpty();
        }

    }
}