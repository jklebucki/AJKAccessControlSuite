using System.ComponentModel.DataAnnotations;

namespace AJKAccessControl.Shared.DTOs
{
    public class DeleteUserDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
    }
}