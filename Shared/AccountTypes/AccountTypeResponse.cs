using Shared.Common;

namespace Shared.AccountTypes
{
    public class AccountTypeResponse : OrganizationDependentResponse<int>
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}