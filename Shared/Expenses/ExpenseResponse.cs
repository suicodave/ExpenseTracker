using Shared.Common;
using Shared.Common.Expenses;

namespace Shared.Expenses
{
    public class ExpenseResponse : OrganizationDependentResponse<int>
    {
        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public DateTime EffectiveDate { get; set; }

        public ExpenseStatus Status { get; set; }

        public IEnumerable<ExpenseAccountResponse> Accounts { get; set; } = default!;

        public decimal UnliquidatedBalance => Amount - Accounts.Sum(x => x.Amount);

        public bool IsBalanced => UnliquidatedBalance == 0;
    }
}