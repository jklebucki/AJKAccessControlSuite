
namespace AJKAccessControl.Shared.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public bool IsCompanyCar { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}