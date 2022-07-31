using System.Data.Entity.ModelConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Expenses;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class ExpenseAccountEntityConfiguration : AuditableEntityConfiguration<ExpenseAccount>
    {
        public override void Configure(EntityTypeBuilder<ExpenseAccount> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

            builder.HasOne(x => x.Expense)
            .WithMany(x => x.Accounts)
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}