using Shared.Common;

namespace Shared.Organizations
{
    public class OrganizationResponse : AuditableEntityResponse<int>
    {
        public string Name { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

        public override bool Equals(object? o)
        {
            var other = o as OrganizationResponse;

            return other?.Id == Id;
        }

        
        public override int GetHashCode() => Id.GetHashCode();

        
        public override string ToString() => Name;
    }
}