using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Budgets;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class BudgetUntrackedAccountConfiguration : IEntityTypeConfiguration<BudgetUntrackedAccount>
    {
        public void Configure(EntityTypeBuilder<BudgetUntrackedAccount> builder)
        {
            builder.HasNoKey();
        }
    }
}