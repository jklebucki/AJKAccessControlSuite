using AJKAccessControl.Application.Services;
using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Data;
using AJKAccessControl.Infrastructure.Repositories;
using AJKAccessControl.Shared.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AJKAccessControlAPI.Configurations
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            AddControllers(services);
            AddSwagger(services);
            AddIdentity(services);
            AddRepositories(services);
            ConfigureDbContext(services, configuration);
            ConfigureJwt(services, configuration);
        }

        private static void AddControllers(IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AJKAccessControlAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    options.User.RequireUniqueEmail = false;
                })
                .AddEntityFrameworkStores<AccessControlDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IAccessEntryRepository, AccessEntryRepository>();
            services.AddScoped<IAccessEntryService, AccessEntryService>();
            services.AddScoped<IChangeLogRepository, ChangeLogRepository>();
            services.AddScoped<IChangeLogService, ChangeLogService>();
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AccessControlDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        private static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            if (jwtSettings == null)
            {
                throw new InvalidOperationException("JWT settings are not configured properly.");
            }
            services.AddSingleton(jwtSettings);
            services.AddJwtAuthentication(configuration);
        }
    }
}