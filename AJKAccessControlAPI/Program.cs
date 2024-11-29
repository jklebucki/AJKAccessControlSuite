using AJKAccessControlAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

ServiceConfiguration.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

await ConfigureWebApplication.SeedData(app);

ConfigureWebApplication.ConfigureMiddleware(app);

app.Run();