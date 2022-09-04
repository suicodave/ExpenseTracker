using Server.Common.Entities;

namespace Server.Budgets
{
    public class BudgetTotalExpensesAndBudget
    {
        public int Id { get; set; }
        
        public DateTime CoveredFrom { get; set; } = DateTime.Now;

        public DateTime CoveredTo { get; set; } = DateTime.Now;

        public decimal TotalExpenses { get; set; }

        public decimal TotalBudget { get; set; }
    }
}