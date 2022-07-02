using System.Net;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Toolbelt.Blazor;

namespace Client.Common
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _httpClientInterceptor;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _stateProvider;

        public HttpInterceptorService(HttpClientInterceptor httpClientInterceptor, NavigationManager navigationManager, ILocalStorageService localStorage, AuthenticationStateProvider stateProvider)
        {
            _httpClientInterceptor = httpClientInterceptor;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
            _stateProvider = stateProvider;
        }

        public void RegisterEvent() => _httpClientInterceptor.AfterSend += new EventHandler<HttpClientInterceptorEventArgs>(
            async (sender, e) => await InterceptResponse(sender!, e)
        );

        private async Task InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            HttpStatusCode statusCode = e.Response.StatusCode;

            Console.WriteLine($"Intercepted with {statusCode}");

            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine("Removing JWT");
                    await _localStorage.RemoveItemAsync("jwt");
                    Console.WriteLine("Refreshing sate");
                    await _stateProvider.GetAuthenticationStateAsync();
                    _navigationManager.NavigateTo("/Auth");
                    break;
                default:
                    break;
            }
        }
    }
}