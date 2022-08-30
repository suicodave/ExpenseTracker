using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Budgets;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class BudgetHeaderConfiguration : AuditableEntityConfiguration<BudgetHeader>
    {

        public override void Configure(EntityTypeBuilder<BudgetHeader> builder)
        {
            base.Configure(builder);

        }

    }
}