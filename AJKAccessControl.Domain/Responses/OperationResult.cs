namespace AJKAccessControl.Domain.Responses
{
    public class OperationResult<T>
    {
        public bool Succeeded { get; set; } = true;
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public T? Data { get; set; }
    }
}