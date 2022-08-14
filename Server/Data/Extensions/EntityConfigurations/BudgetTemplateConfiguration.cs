using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Budgets;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class BudgetTemplateConfiguration : AuditableEntityConfiguration<BudgetTemplate>
    {
        public override void Configure(EntityTypeBuilder<BudgetTemplate> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.AccountType)
            .WithMany(x => x.BudgetTemplate)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

        }
    }
}