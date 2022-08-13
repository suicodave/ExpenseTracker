using Shared.Organizations;

namespace Shared.Common
{
    public class OrganizationDependentResponse<TKey> : AuditableEntityResponse<TKey>
    where TKey : struct
    {
        public int OrganizationId { get; set; }

        public OrganizationResponse Organization { get; set; } = default!;
    }

    public class OrganizationDependentResponse
    {
        public int OrganizationId { get; set; }
        
        public OrganizationResponse Organization { get; set; } = default!;
    }
}