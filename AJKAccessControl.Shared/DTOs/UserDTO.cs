namespace AJKAccessControl.Shared.DTOs
{
    public class UserDto
    {
        public string? Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;   
        public IList<string?> Roles { get; set; } = new List<string?>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}