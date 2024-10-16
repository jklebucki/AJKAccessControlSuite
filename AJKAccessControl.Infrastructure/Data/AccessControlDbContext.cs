
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

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<AccessEntry> AccessEntries { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracje encji, np. klucze główne, relacje
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccessControlDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
