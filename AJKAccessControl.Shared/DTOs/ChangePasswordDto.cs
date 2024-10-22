using System.ComponentModel.DataAnnotations;

namespace AJKAccessControl.Shared.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}