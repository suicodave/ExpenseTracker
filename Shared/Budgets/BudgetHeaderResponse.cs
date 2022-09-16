using System.Collections;

using Shared.Common;

namespace Shared.Budgets
{
    public class BudgetHeaderResponse : IBudgetWithExpenseResponse
    {
        public int Id { get; set; }

        public DateTime CoveredFrom { get; set; }

        public DateTime CoveredTo { get; set; }

        public decimal Budget { get; set; }

        public decimal Expenses { get; set; }

        public decimal Percent { get; set; }

        public decimal Balance { get; set; }
    }
}