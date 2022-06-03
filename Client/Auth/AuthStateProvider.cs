using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name,"Dave")
            };

            var identity = new ClaimsIdentity(claims,"jwt");

            var user = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return Task.FromResult(state);
        }
    }
}