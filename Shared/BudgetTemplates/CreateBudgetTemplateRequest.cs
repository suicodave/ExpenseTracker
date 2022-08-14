namespace Shared.BudgetTemplates
{
    public class CreateBudgetTemplateRequest
    {
        public int AccountTypeId { get; set; }

        public decimal Amount { get; set; } = 0;
    }
}