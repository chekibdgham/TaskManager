using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    [Index(nameof(Username), IsUnique = true)] // Unique Index
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Username field is required")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; } // "Admin" or "User"
        
        [Required]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
        public string Password { get; internal set; } = string.Empty;

        //public virtual ICollection<TaskToDo> TaskItems { get; set; } = new HashSet<TaskToDo>();
    }

    public enum UserRole
    {
        Admin,
        User
    }

    
    
}
