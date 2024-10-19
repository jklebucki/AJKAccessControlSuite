namespace AJKAccessControl.Domain.Responses
{
    public class OperationResult
    {
        public bool Succeeded { get; set; } = true;
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public string Data { get; set; } = string.Empty;
    }
}