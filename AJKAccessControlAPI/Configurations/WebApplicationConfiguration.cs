using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Identity;
using AJKAccessControlAPI.Middleware;
using Microsoft.AspNetCore.Identity;

namespace AJKAccessControlAPI.Configurations
{
    public static class ConfigureWebApplication
    {
        public static async Task SeedData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var identityRoleSeeder = new IdentityRoleSeeder(roleManager);
                var identityUserSeeder = new IdentityUserSeeder(userManager, roleManager);
                await identityRoleSeeder.CreateDefaultRoles();
                await identityUserSeeder.CreateDefaultUsers();
            }
        }

        public static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AJKAccessControlAPI v1"));

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting()
                .UseMiddleware<RequestLoggingMiddleware>()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}