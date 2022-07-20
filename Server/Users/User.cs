using System.Collections;

using Microsoft.AspNetCore.Identity;

namespace Server.Users
{
    public class User : IdentityUser<int>
    {
        public string? DisplayName { get; set; }

        public ICollection<UserOrganization> UserOrganizations { get; set; } = new List<UserOrganization>();
    }
}