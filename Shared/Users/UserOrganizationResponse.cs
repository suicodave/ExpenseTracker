using Shared.Common;
using Shared.Organizations;

namespace Shared.Users
{
    public class UserOrganizationResponse : OrganizationDependentResponse<int>
    {
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