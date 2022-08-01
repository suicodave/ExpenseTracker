namespace Shared.Expenses
{
    public class CreateExpenseAccountRequest
    {
        public int AccountTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}