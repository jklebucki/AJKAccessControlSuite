namespace AJKAccessControl.Domain.Entities
{

    public class Employee
    {
        public int Id { get; set; }
        public EntitityType Type { get; } = EntitityType.Employee;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

    }
}