namespace Shared.Budgets
{
    public class BudgetAccountWithExpenseResponse
    {
        public string AccountTypeName { get; set; } = string.Empty;

        public decimal TotalExpenses { get; set; }

        public decimal Budget { get; set; }

        public decimal Percent { get; set; }
    }
}