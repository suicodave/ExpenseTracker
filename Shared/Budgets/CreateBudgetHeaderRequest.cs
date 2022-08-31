namespace Shared.Budgets
{
    public class CreateBudgetHeaderRequest
    {
        public DateTime? CoveredFrom { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        public DateTime? CoveredTo { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

        public IList<BudgetAccountRequest> BudgetAccounts { get; set; } = new List<BudgetAccountRequest>();
    }
}