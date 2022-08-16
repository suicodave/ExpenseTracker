using Shared.AccountTypes;
using Shared.Common;

namespace Shared.BudgetTemplates
{
    public class BudgetTemplateResponse : AuditableEntityResponse<int>
    {
        public AccountTypeResponse AccountType { get; set; } = default!;

        public int AccountTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}