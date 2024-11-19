namespace AJKAccessControl.Domain.Entities
{

    public class Person
    {
        public int Id { get; set; }
        public EntitityType Type { get; } = EntitityType.Person;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsEmployee { get; set; } = true;
        private string _company = string.Empty;
        public string Company
        {
            get => _company;
            set
            {
                if (IsEmployee && string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Company is required when IsEmployee is true.");
                }
                _company = value;
            }
        }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}