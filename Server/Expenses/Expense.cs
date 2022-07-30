using Server.Common.Entities;
using Server.Organizations;

namespace Server.Expenses
{
    public class Expense : AuditableEntity<int>
    {
        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;

        public DateTime EffectiveDate { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }

    public enum ExpenseStatus
    {
        Pending,
        Completed,
    }
}