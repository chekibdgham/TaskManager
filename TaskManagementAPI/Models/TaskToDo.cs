
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementAPI.Models
{
    public class TaskToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public TStatus Status { get;  set; } // "Open", "In Progress", "Closed"

         
        public int AssignedUserId { get; set; }

        
        public void UpdateStatus(TStatus status)=>Status=status;

        internal void Update(TaskToDo task)
        {
            Title = task.Title;
            Description = task.Description;
            Status = task.Status;
            AssignedUserId = task.AssignedUserId;
        }
    }

    public enum TStatus
    {
        Open=0,
        InProgress=1,
        Closed=2
    }
}
