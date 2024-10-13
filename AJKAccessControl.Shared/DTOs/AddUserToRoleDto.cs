namespace AJKAccessControl.Shared.DTOs
{
    public class AddUserToRoleDto
    {
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}