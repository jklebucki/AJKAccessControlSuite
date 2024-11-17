using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AJKAccessControl.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public override string UserName { get; set; } = string.Empty;
        public override string? Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IList<string?> Roles { get; set; } = [];
        public User()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}