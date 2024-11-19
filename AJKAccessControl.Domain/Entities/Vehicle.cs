namespace AJKAccessControl.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public EntitityType EntitityType { get; } = EntitityType.Vehicle;
        public string PlateNumber { get; set; } = string.Empty;
        public bool IsCompanyCar { get; set; } = false;
        private string _company = string.Empty;
        public string Company
        {
            get => _company;
            set
            {
                if (IsCompanyCar && string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Company is required when IsCompanyCar is true.");
                }
                _company = value;
            }
        }
        public string Owner { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}