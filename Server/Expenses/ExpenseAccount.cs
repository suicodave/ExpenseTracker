using Server.AccountTypes;
using Server.Common.Entities;

namespace Server.Expenses
{
    public class ExpenseAccount : AuditableEntity<int>
    {
        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }

        public int ExpenseId { get; set; }

        public Expense Expense { get; set; }
        
        public decimal Amount { get; set; }
    }
}