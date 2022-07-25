namespace Shared.Expenses
{
    public class CreateExpenseRequest
    {
        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;
    }
}