namespace AJKAccessControl.Domain.Entities
{

    public class AccessEntry
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public EntitityType EntityType { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.UtcNow;
        public DateTime? ExitTime { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}