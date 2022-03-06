using CarFleetUI;
//using CarFleetUI.Components;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.RootComponents.RegisterForJavaScript<GlobalAlert>(identifier: "globalAlert");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddHttpClient<IVehicleService, VehicleService>(client =>
    client.BaseAddress = new Uri("https://localhost:7228/"));
builder.Services.AddHttpClient<IDriverService, DriverService>(client =>
    client.BaseAddress = new Uri("https://localhost:7228/"));
builder.Services.AddHttpClient<IAssignmentService, AssignmentService>(client =>
    client.BaseAddress = new Uri("https://localhost:7228/"));


var app = builder.Build();

//ConfigureEndpoints(app, app.Services);
//app.MapGet("/", () => "Hello World!");

await app.RunAsync();

//void ConfigureEndpoints(WebAssemblyHost app, IServiceProvider services) => MapFallbackToPage("/_Host");

