
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Users;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x=>x.DisplayName)
            .HasMaxLength(100);
        }
    }
}