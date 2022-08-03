using Shared.AccountTypes;

namespace Shared.Expenses
{
    public class ExpenseAccountResponse
    {
        public AccountTypeResponse AccountType { get; set; }

        public decimal Amount { get; set; }
    }
}