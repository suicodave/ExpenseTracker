using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Server.Common.Entities;

namespace Server.Data.Extensions.EntityConfigurations
{
    public class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : AuditableEntity
    {

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}