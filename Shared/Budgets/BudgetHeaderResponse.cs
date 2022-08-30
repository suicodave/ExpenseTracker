using Shared.Common;

namespace Shared.Budgets
{
    public class BudgetHeaderResponse : OrganizationDependentResponse
    {
        public DateTime CoveredFrom { get; set; }

        public DateTime CoveredTo { get; set; }
    }
}