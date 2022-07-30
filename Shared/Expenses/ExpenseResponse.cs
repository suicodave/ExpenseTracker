using Shared.Common;

namespace Shared.Expenses
{
    public class ExpenseResponse : AuditableEntityResponse<int>
    {
        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public DateTime EffectiveDate { get; set; }

    }
}