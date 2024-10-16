using AJKAccessGuard;
using AJKAccessGuard.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
{
    BaseAddress = new Uri("https://localhost:7277")
});

builder.Services.AddScoped<IUsersApiService, UserApiService>();

await builder.Build().RunAsync();
