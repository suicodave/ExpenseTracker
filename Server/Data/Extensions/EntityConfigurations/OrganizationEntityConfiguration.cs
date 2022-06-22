using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Common.Entities;
using Server.Organizations;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
    {

        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasAlternateKey(x => new { x.UserId, x.Name });
        }
    }
}