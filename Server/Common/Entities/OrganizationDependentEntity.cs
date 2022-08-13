using Server.Organizations;

namespace Server.Common.Entities
{
    public class OrganizationDependentEntity<TKey> : AuditableEntity<TKey>
    where TKey : struct
    {
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; } = default!;
    }

    public class OrganizationDependentEntity
    {
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; } = default!;
    }
}