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
        private readonly ILogger<HttpInterceptorService> _logger;

        public HttpInterceptorService(HttpClientInterceptor httpClientInterceptor, NavigationManager navigationManager, ILocalStorageService localStorage, AuthenticationStateProvider stateProvider, ILogger<HttpInterceptorService> logger)
        {
            _httpClientInterceptor = httpClientInterceptor;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
            _stateProvider = stateProvider;
            _logger = logger;
        }

        public void RegisterEvent() => _httpClientInterceptor.AfterSend += new EventHandler<HttpClientInterceptorEventArgs>(
            async (sender, e) => await InterceptResponse(sender!, e)
        );

        private async Task InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            HttpStatusCode statusCode = e.Response.StatusCode;

            _logger.LogInformation("Received request with status: {statusCode}", statusCode);

            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _logger.LogInformation("Logging out.");

                    
                    _navigationManager.NavigateTo("LogOut");
                    
                    break;
                default:
                    break;
            }
        }
    }
}