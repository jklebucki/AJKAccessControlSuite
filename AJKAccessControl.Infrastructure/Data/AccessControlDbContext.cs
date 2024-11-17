
using AJKAccessControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AJKAccessControl.Infrastructure.Data
{
    public class AccessControlDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AccessControlDbContext(DbContextOptions<AccessControlDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<AccessEntry> AccessEntries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccessControlDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
