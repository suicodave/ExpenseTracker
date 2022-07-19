using Shared.Common;
using Shared.Organizations;

namespace Shared.Users
{
    public class UserOrganizationResponse : AuditableEntityResponse<int>
    {
        public OrganizationResponse Organization { get; set; }

        public bool IsDefault { get; set; }

        public override bool Equals(object? o)
        {
            var other = o as UserOrganizationResponse;

            return other?.Id == Id;
        }


        public override int GetHashCode() => Id.GetHashCode();


        public override string ToString() => Organization.Name;
    }
}