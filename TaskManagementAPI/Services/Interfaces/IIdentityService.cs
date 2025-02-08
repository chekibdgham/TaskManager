using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface IIdentityService
    {
        int GetCurrentUserId();
        UserRole GetCurrentUserRole();
    }
}
