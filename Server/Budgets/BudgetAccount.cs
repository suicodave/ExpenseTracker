namespace Server.Budgets
{
    public class BudgetAccount
    {
        public int Id { get; set; }
        
        public int AccountTypeId { get; set; }

        public decimal Amount { get; set; }

        public int BudgetHeaderId { get; set; }

        public BudgetHeader BudgetHeader { get; set; } = default!;
    }
}