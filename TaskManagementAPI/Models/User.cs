namespace TaskManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public UserRole Role { get; set; } // "Admin" or "User"
    }

    public enum UserRole
    {
        Admin,
        User
    }
}
