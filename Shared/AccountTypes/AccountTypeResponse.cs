using Shared.Common;

namespace Shared.AccountTypes
{
    public class AccountTypeResponse : AuditableEntityResponse<int>
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int OrganizationId { get; set; }
    }
}