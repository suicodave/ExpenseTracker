using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Expenses;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class ExpenseEntityConfiguration : AuditableEntityConfiguration<Expense>
    {
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Status)
           .HasConversion<string>();

            builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

            builder.HasMany(x=>x.Accounts)
            .WithOne(x=>x.Expense);
        }
    }
}