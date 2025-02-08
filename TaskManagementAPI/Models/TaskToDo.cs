namespace TaskManagementAPI.Models
{
    public class TaskToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public TStatus Status { get; set; } // "Open", "In Progress", "Closed"
        public int AssignedUserId { get; set; }
    }

    public enum TStatus
    {
        Open,
        InProgress,
        Closed
    }
}
