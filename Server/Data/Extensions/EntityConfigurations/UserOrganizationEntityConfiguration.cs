using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Users;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class UserOrganizationEntityConfiguration : IEntityTypeConfiguration<UserOrganization>
    {
        public void Configure(EntityTypeBuilder<UserOrganization> builder)
        {
            builder.HasIndex(x=> new {x.UserId, x.OrganizationId})
            .IsUnique();
        }
    }
}