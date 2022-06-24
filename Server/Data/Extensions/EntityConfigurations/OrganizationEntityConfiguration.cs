using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Common.Entities;
using Server.Organizations;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class OrganizationEntityConfiguration : AuditableEntityConfiguration<Organization>
    {

        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => new { x.UserId, x.Name });
        }
    }
}