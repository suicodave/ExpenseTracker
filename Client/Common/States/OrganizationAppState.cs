using Microsoft.AspNetCore.Components;

using Shared.Users;

namespace Client.Common.States
{
    public class OrganizationAppState
    {
        public IEnumerable<UserOrganizationResponse>? Organizations { get; private set; } = null;

        public UserOrganizationResponse? DefaultOrganization => Organizations?.Where(x => x.IsDefault).FirstOrDefault();

        public void UpdateOrganizations(ComponentBase source, IEnumerable<UserOrganizationResponse> organizations)
        {
            this.Organizations = organizations;

            NotifyStateChanged(source, organizations);

        }

        public event Action<ComponentBase, IEnumerable<UserOrganizationResponse>>? StateChanged;

        public void NotifyStateChanged(ComponentBase source, IEnumerable<UserOrganizationResponse> organizations) => StateChanged?.Invoke(source, organizations);

    }
}