using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Microsoft.AspNetCore.Components.Authorization;
using Client.Auth;
using Client;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Client.Common;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClientInterceptor();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api/")
}.EnableIntercept(sp)
);

builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();


var app = builder.Build();


await app.RunAsync();