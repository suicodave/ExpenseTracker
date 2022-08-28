using Server.Organizations;

namespace Server.Common.Entities
{
    public interface IOrganizationDependentEntity
    {
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }

    public class OrganizationDependentEntity<TKey> : AuditableEntity<TKey>, IOrganizationDependentEntity
    where TKey : struct
    {
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; } = default!;
    }

    public class OrganizationDependentEntity : IOrganizationDependentEntity
    {
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; } = default!;
    }

}