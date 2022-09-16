namespace Shared.Budgets
{
    public interface IBudgetWithExpenseResponse
    {
        public decimal Budget { get; set; }

        public decimal Expenses { get; set; }

        public decimal Percent { get; set; }

        public decimal Balance { get; set; }
    }
}