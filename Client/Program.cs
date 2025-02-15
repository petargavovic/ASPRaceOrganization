using BaseLibrary.DTOs.EntityDTOs;
using BaseLibrary.Entities;
using Blazored.LocalStorage;
using Client;
using Client.ApplicationStates;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using Syncfusion.Licensing;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF1cXGdCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXZfeHRcQ2VdUkByXUo=");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7037/");
}).AddHttpMessageHandler<CustomHttpHandler>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7298/") });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

builder.Services.AddScoped<IGenericServiceInterface<VozacDTO>, GenericServiceImplementation<VozacDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<TrkaDTO>, GenericServiceImplementation<TrkaDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<RangListaDTO>, GenericServiceImplementation<RangListaDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<UcinakDTO>, GenericServiceImplementation<UcinakDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<PlasmanDTO>, GenericServiceImplementation<PlasmanDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<Kategorija>, GenericServiceImplementation<Kategorija>>();

builder.Services.AddScoped<AllState>();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();

await builder.Build().RunAsync();
