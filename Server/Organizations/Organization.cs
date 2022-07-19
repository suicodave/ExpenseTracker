using Server.Common.Entities;
using Server.Users;

namespace Server.Organizations
{
    public class Organization : AuditableEntity<int>
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<UserOrganization> UserOrganizations { get; set; } = new List<UserOrganization>();
    }
}