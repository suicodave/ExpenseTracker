using Shared.Users;

namespace Shared.Member
{
    public class CreateMemberRequest : RegisterUserRequest
    {
        public string RoleName { get; set; }
    }
}