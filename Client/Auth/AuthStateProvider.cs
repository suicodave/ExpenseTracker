using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public AuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            string jwt = await _localStorage.GetItemAsStringAsync("jwt");

            if (jwt is null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            IEnumerable<Claim> claims = new JwtSecurityToken(jwt).Claims;

            ClaimsIdentity identity = new ClaimsIdentity(claims, "jwt");

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(principal);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}