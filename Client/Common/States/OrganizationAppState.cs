using System.Net.Http.Json;

using Microsoft.AspNetCore.Components;

using Shared.Organizations;
using Shared.Users;

namespace Client.Common.States
{
    public class OrganizationAppState
    {
        private readonly HttpClient _http;

        public OrganizationAppState(HttpClient http)
        {
            _http = http;
        }

        public IEnumerable<UserOrganizationResponse>? Organizations { get; private set; } = null;

        public UserOrganizationResponse? DefaultOrganization => Organizations?.Where(x => x.IsDefault).FirstOrDefault();

        public void UpdateOrganizations(ComponentBase source, IEnumerable<UserOrganizationResponse> organizations)
        {
            this.Organizations = organizations;

            NotifyStateChanged();

        }

        public event Action? StateChanged;

        public void NotifyStateChanged() => StateChanged?.Invoke();


        public async Task GetData()
        {
            Organizations = await _http.GetFromJsonAsync<IEnumerable<UserOrganizationResponse>>("Organizations/Owned") ??
            Enumerable.Empty<UserOrganizationResponse>();

        }

        public async Task LoadWhenEmpty()
        {
            if (Organizations is null)
            {
                await GetData();
            }
        }

        public async Task<HttpResponseMessage> SetAsDefault(int id)
        {
            string path = $"Organizations/{id}/SetDefault";

            var response = await _http.PutAsync(path, null);

            if (response.IsSuccessStatusCode)
            {
                UserOrganizationResponse currentDefaultOrg = Organizations!.Where(x => x.IsDefault).First();

                currentDefaultOrg.IsDefault = false;

                UserOrganizationResponse selectedOrg = Organizations!.Where(x => x.Id == id).First();

                selectedOrg.IsDefault = true;

                NotifyStateChanged();
            }


            return response;
        }

        public async Task<HttpResponseMessage> CreateUserOrganization(OrganizationRequest request){
            var response = await _http.PostAsJsonAsync("Organizations", request);

            if (response.IsSuccessStatusCode)
            {
                await GetData();
            }

            return response;
        }

    }
}