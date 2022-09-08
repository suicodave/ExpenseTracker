namespace Server.Budgets
{
    public class BudgetAccountWithExpense
    {
        public string AccountTypeName { get; set; } = string.Empty;

        public decimal TotalExpenses { get; set; }

        public decimal Budget { get; set; }
    }
}