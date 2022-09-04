using System.Collections;

using Shared.Common;

namespace Shared.Budgets
{
    public class BudgetHeaderResponse
    {
        public int Id { get; set; }

        public DateTime CoveredFrom { get; set; }

        public DateTime CoveredTo { get; set; }

        public decimal TotalBudget { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal ExpensePercent { get; set; }

        public decimal RemainingBalance { get; set; }

    }
}