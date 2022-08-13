using System.Collections.Generic;

using Server.Common.Entities;
using Server.Organizations;

using Shared.Common.Expenses;

namespace Server.Expenses
{
    public class Expense : OrganizationDependentEntity<int>
    {
        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;

        public DateTime EffectiveDate { get; set; }

        public ICollection<ExpenseAccount> Accounts { get; set; }
    }
}