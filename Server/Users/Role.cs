using Microsoft.AspNetCore.Identity;

namespace Server.Users
{
    public class Role : IdentityRole<int>
    {
        public static string OWNER = "Owner";
    }
}