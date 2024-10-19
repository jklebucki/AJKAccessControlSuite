using System.ComponentModel.DataAnnotations;

namespace AJKAccessControl.Shared.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
    }
}