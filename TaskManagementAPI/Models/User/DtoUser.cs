using System.ComponentModel;

namespace TaskManagementAPI.Models.User;

public class DtoUser
{
    public int UserId { get; set; }
    [DisplayName("User")]
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    //public virtual ICollection<DtoTask> taskList { get; set; } = new List<DtoTask>();

}

//public class DtoTask
//{
//    //public int TaskId { get; set; }
//    public string? TaskName { get; set; }
//    public string? Description { get; set; } = string.Empty;
//    public string? Status { get; set; } = string.Empty;
//    //public int UserId { get; set; }
//}
