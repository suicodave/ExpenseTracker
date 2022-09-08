using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Budgets;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class BudgetAccountWithExpenseConfiguration : IEntityTypeConfiguration<BudgetAccountWithExpense>
    {
        public void Configure(EntityTypeBuilder<BudgetAccountWithExpense> builder)
        {
            builder.HasNoKey();
        }
    }
}