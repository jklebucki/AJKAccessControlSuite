namespace AJKAccessControl.Domain.Entities
{

    public class AccessEntry
    {
        public int Id { get; set; }
        public int EntitityId { get; set; }
        public EntitityType EntitityType { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.UtcNow;
        public DateTime? ExitTime { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}