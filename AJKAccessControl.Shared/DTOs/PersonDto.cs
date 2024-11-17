namespace AJKAccessControl.Shared.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsEmployee { get; set; }
        public string Company { get; set; } = string.Empty;
    }
}