using Server.Common.Entities;
using Server.Organizations;

namespace Server.Users
{
    public class UserOrganization : OrganizationDependentEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}