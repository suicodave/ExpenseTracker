using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Budgets;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class BudgetTotalExpensesAndBudgetEntityConfiguration : IEntityTypeConfiguration<BudgetTotalExpensesAndBudget>
    {
        public void Configure(EntityTypeBuilder<BudgetTotalExpensesAndBudget> builder)
        {
            builder.HasNoKey();
        }
    }
}