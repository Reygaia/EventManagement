namespace EventManagement.DTO
{
    public enum TaskType
    {
        Check,
        Count
    }


    public class TaskDTO
    {
        public string Description { get; set; }
        public TaskType Type { get; set; }
        public int? MaxCount { get; set; }
        public int? Count { get; set; }
        public DateTime Deadline { get; set; }
    }
}
