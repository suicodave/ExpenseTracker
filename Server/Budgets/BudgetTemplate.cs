using Server.AccountTypes;
using Server.Common.Entities;

namespace Server.Budgets
{
    public class BudgetTemplate : OrganizationDependentEntity<int>
    {
        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; } = default!;

        public decimal Amount { get; set; } = 0;
    }
}