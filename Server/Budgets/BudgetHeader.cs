using Server.Common.Entities;

namespace Server.Budgets
{
    public class BudgetHeader : OrganizationDependentEntity<int>
    {
        public DateTime CoveredFrom { get; set; } = DateTime.Now;

        public DateTime CoveredTo { get; set; } = DateTime.Now;

        public ICollection<BudgetAccount> BudgetAccounts { get; set; } = new List<BudgetAccount>();
    }
}