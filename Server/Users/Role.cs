using Microsoft.AspNetCore.Identity;

namespace Server.Users
{
    public class Role : IdentityRole<int>
    {
        public static string OWNER = "Owner";

        public static string ADMINISTRATOR = "Administrator";

        public static string CLERK = "Clerk";

        public static string ACCOUNTANT = "Accountant";

    }
}