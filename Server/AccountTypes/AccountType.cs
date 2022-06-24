using Server.Common.Entities;
using Server.Organizations;

namespace Server.AccountTypes
{
    public class AccountType : AuditableEntity<int>
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}