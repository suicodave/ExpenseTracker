using System.Collections;

using Shared.Common;

namespace Shared.Budgets
{
    public class BudgetHeaderResponse : OrganizationDependentResponse
    {
        public DateTime CoveredFrom { get; set; }

        public DateTime CoveredTo { get; set; }

        public decimal TotalBudget { get; set; }

        public IEnumerable<BudgetAccountResponse> BudgetAccounts { get; set; } = Enumerable.Empty<BudgetAccountResponse>();
    }
}