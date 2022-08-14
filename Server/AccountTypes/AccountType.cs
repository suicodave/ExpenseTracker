using Server.Budgets;
using Server.Common.Entities;
using Server.Organizations;

namespace Server.AccountTypes
{
    public class AccountType : OrganizationDependentEntity<int>
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<BudgetTemplate> BudgetTemplate { get; set; } = default!;
    }
}