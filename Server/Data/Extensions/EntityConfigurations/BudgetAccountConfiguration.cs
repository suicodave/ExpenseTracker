using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Budgets;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class BudgetAccountConfiguration : IEntityTypeConfiguration<BudgetAccount>
    {
        public void Configure(EntityTypeBuilder<BudgetAccount> builder)
        {
            builder.HasOne(x => x.BudgetHeader)
            .WithMany(x => x.BudgetAccounts);

            builder.Property(x => x.Amount)
            .HasPrecision(18, 2);
        }
    }
}