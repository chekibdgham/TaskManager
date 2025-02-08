using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Middelware;

public class RoleBasedAccessControlMiddleware
{
    private readonly RequestDelegate _next;

    public RoleBasedAccessControlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, TaskManagementDB dbContext)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        var roleClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Role");

        if (userIdClaim != null && roleClaim != null)
        {
            var userId = int.Parse(userIdClaim.Value);
            var role = (UserRole)Enum.Parse(typeof(UserRole), roleClaim.Value);

            context.Items["UserId"] = userId;
            context.Items["UserRole"] = role;

            if (role == UserRole.User)
            {
                var path = context.Request.Path.Value;
                if (path.Contains("/tasks", StringComparison.OrdinalIgnoreCase))
                {
                    var taskId = int.Parse(path.Split('/').Last());
                    var task = dbContext.Tasks.Find(taskId);

                    if (task == null || task.AssignedUserId != userId)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Access denied. You can only access tasks assigned to you.");
                        return;
                    }
                }
            }
        }

        await _next(context);
    }
}
