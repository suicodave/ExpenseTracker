using Shared.Common;

namespace Shared.BudgetHeaders
{
    public class BudgetHeaderResponse : OrganizationDependentResponse
    {
        public DateTime CoveredFrom { get; set; }

        public DateTime CoveredTo { get; set; }
    }
}