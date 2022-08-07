using Shared.AccountTypes;
using Shared.Common;

namespace Shared.Expenses
{
    public class ExpenseAccountResponse : AuditableEntityResponse<int>
    {
        public AccountTypeResponse AccountType { get; set; }

        public decimal Amount { get; set; }
    }
}