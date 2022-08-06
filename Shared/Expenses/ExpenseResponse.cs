using Shared.Common;

namespace Shared.Expenses
{
    public class ExpenseResponse : AuditableEntityResponse<int>
    {
        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public DateTime EffectiveDate { get; set; }

        public IEnumerable<ExpenseAccountResponse> Accounts { get; set; }

        public decimal UnliquidatedBalance => Amount - Accounts.Sum(x => x.Amount);

        public bool IsBalanced => UnliquidatedBalance == 0;

    }
}