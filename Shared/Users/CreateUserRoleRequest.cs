using System.Collections;

namespace Shared.Users
{
    public class SyncUserRoleRequest
    {
        public string UserEmail { get; set; } = string.Empty;

        public IEnumerable<string> RoleNames { get; set; } = new List<string>();
    }
}