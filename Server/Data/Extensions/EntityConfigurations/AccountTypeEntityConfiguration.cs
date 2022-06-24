using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.AccountTypes;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class AccountTypeEntityConfiguration : AuditableEntityConfiguration<AccountType>
    {
        public override void Configure(EntityTypeBuilder<AccountType> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
            .HasMaxLength(50);

            builder.HasIndex(x => x.Name)
            .IsUnique();

        }
    }
}