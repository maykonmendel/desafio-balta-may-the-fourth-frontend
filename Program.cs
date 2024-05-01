using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MayTheFourth;
using MayTheFourth.Services;
using MayTheFourth.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(
    Configuration.HttpClientName,
    x =>
    {
        x.BaseAddress = new Uri(Configuration.BackendUrl);
    });

builder.Services.AddTransient<MovieService>();
builder.Services.AddTransient<CharacterService>();
builder.Services.AddTransient<VehicleService>();
builder.Services.AddTransient<PlanetService>();
builder.Services.AddTransient<StarshipService>();

await builder.Build().RunAsync();
