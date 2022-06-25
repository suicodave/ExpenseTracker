using Shared.Common;

namespace Shared.Organizations
{
    public class OrganizationResponse : AuditableEntityResponse<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}