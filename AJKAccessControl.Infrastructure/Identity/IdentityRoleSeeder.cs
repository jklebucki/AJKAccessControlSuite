using Microsoft.AspNetCore.Identity;

namespace AJKAccessControl.Infrastructure.Identity
{
    public static class IdentityRoleSeeder
    {
        public static IEnumerable<IdentityRole<Guid>> GetDefaultRoles()
        {
            return new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = Role.Admin.ToString(), NormalizedName = Role.Admin.ToString().ToUpper() },
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = Role.Supervisor.ToString(), NormalizedName = Role.Supervisor.ToString().ToUpper() },
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = Role.User.ToString(), NormalizedName = Role.User.ToString().ToUpper() }
            };
        }
    }
}