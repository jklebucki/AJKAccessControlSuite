using Microsoft.AspNetCore.Identity;

namespace AJKAccessControl.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}