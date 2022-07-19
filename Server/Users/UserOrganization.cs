using Server.Organizations;

namespace Server.Users
{
    public class UserOrganization
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}