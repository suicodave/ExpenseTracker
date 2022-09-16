namespace Shared.Budgets
{
    public class BudgetAccountWithExpenseResponse : IBudgetWithExpenseResponse
    {
        public string AccountTypeName { get; set; } = string.Empty;

        public decimal Budget { get; set; }

        public decimal Percent { get; set; }

        public decimal Expenses { get; set; }

        public decimal Balance { get; set; }
    }
}