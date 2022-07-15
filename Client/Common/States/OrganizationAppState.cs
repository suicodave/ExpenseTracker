using Microsoft.AspNetCore.Components;

using Shared.Organizations;

namespace Client.Common.States
{
    public class OrganizationAppState
    {
        public IEnumerable<OrganizationResponse>? Organizations { get; private set; } = null;

        public OrganizationResponse? DefaultOrganization => Organizations?.Where(x => x.IsDefault).FirstOrDefault();

        public void UpdateOrganizations(ComponentBase source, IEnumerable<OrganizationResponse> organizations)
        {
            this.Organizations = organizations;

            NotifyStateChanged(source, organizations);

        }

        public event Action<ComponentBase, IEnumerable<OrganizationResponse>>? StateChanged;

        public void NotifyStateChanged(ComponentBase source, IEnumerable<OrganizationResponse> organizations) => StateChanged?.Invoke(source, organizations);

    }
}