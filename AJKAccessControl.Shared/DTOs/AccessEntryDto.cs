namespace AJKAccessControl.Shared.DTOs
{
    public class AccessEntryDto
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public EntitityType EntityType { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.UtcNow;
        public DateTime? ExitTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}