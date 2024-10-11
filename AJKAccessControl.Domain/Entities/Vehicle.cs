namespace AJKAccessControl.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public EntitityType EntitityType { get; } = EntitityType.Vehicle;
        public string PlateNumber { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}