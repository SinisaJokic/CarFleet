using Blazored.LocalStorage;
using CarFleetUI;
//using CarFleetUI.Components;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.RootComponents.RegisterForJavaScript<GlobalAlert>(identifier: "globalAlert");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<ILocalStorageServiceJS, LocalStorageServiceJS>();


builder.Services.AddHttpClient<IVehicleService, VehicleService>(client =>
    client.BaseAddress = new Uri("https://localhost:7228/"));
builder.Services.AddHttpClient<IDriverService, DriverService>(client =>
    client.BaseAddress = new Uri("https://localhost:7228/"));
builder.Services.AddHttpClient<IAssignmentService, AssignmentService>(client =>
    client.BaseAddress = new Uri("https://localhost:7228/"));
builder.Services.AddHttpClient<IUserService, UserService>(client =>
    client.BaseAddress = new Uri("https://localhost:7228/"));
//builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
//    client.BaseAddress = new Uri("https://localhost:7228/"));
//builder.Services.AddHttpClient<ILocalStorageService, LocalStorageService>(client =>
//    client.BaseAddress = new Uri("https://localhost:7228/"));
//builder.Services.AddHttpClient<IHttpService, HttpService>(client =>
//    client.BaseAddress = new Uri("https://localhost:7228/"));

builder.Services.AddApiAuthorization();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

//ConfigureEndpoints(app, app.Services);
//app.MapGet("/", () => "Hello World!");
//var authenticationService = app.Services.GetRequiredService<IAuthenticationService>();
//await authenticationService.Initialize();

await app.RunAsync();

//void ConfigureEndpoints(WebAssemblyHost app, IServiceProvider services) => MapFallbackToPage("/_Host");

