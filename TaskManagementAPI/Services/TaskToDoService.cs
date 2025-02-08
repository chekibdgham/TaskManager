using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TaskToDoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskToDoService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            if (_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Sid)?.Value is string userIdStr && int.TryParse(userIdStr, out int userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("UserId is not set in the current context.");
        }

        private UserRole GetCurrentUserRole()
        {
            if (_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value is string userRoleStr && Enum.TryParse(userRoleStr, out UserRole userRole))
            {
                return userRole;
            }
            throw new UnauthorizedAccessException("UserRole is not set in the current context.");
        }

        public IEnumerable<TaskToDo> GetAll()
        {
            if (GetCurrentUserRole() == UserRole.Admin)
            {
                return _unitOfWork.Tasks.GetAll();
            }
            throw new UnauthorizedAccessException("Only Admin can view all tasks.");
        }

        public TaskToDo GetById(int id)
        {
            var task = _unitOfWork.Tasks.GetById(id);
            if (GetCurrentUserRole() == UserRole.Admin || task.AssignedUserId == GetCurrentUserId())
            {
                return task;
            }
            throw new UnauthorizedAccessException("You can only view tasks assigned to you.");
        }

        public void Add(TaskToDo task)
        {
            if (GetCurrentUserRole() == UserRole.Admin)
            {
                _unitOfWork.Tasks.Add(task);
                _unitOfWork.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("Only Admin can create tasks.");
            }
        }

        public void Update(TaskToDo task)
        {
            var existingTask = _unitOfWork.Tasks.GetById(task.Id);

            if (existingTask is null)
            {
                throw new ArgumentException("Task not found", nameof(task.Id));
            }

            if (GetCurrentUserRole() == UserRole.Admin)
            {
                _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();
            }
            
            else
            {
                throw new UnauthorizedAccessException("You can only update the status of your assigned tasks.");
            }
        }
        public void UpdateTaskStatus(int id, TStatus newTaskStatus)
        {
            var task = _unitOfWork.Tasks.GetById(id);
            if(task is null)
            {
                throw new ArgumentException("Task not found", nameof(id));
            }
                
            if (GetCurrentUserRole() == UserRole.Admin || task.AssignedUserId == GetCurrentUserId())
            {
                task.UpdateStatus(newTaskStatus);
                _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();
            }
            
            else
            {
                throw new UnauthorizedAccessException("You can only update the status of your assigned tasks.");
            }
        }
        public void Delete(int id)
        {
            if (GetCurrentUserRole() == UserRole.Admin)
            {
                _unitOfWork.Tasks.Delete(id);
                _unitOfWork.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("Only Admin can delete tasks.");
            }
        }

        public IEnumerable<TaskToDo> GetByUserId(int userId) => _unitOfWork.Tasks.GetByUserId(userId);
    }
}
