
namespace AJKAccessControl.Domain.Entities
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public string ChangedBy { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}