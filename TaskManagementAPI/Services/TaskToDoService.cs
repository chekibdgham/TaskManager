﻿using System.Collections.Generic;
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
            if (_httpContextAccessor?.HttpContext?.Items["UserId"] is int userId)
            {
                return userId;
            }
            throw new UnauthorizedAccessException("UserId is not set in the current context.");
        }

        private UserRole GetCurrentUserRole()
        {
            if (_httpContextAccessor?.HttpContext?.Items["UserRole"] is UserRole userRole)
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
            if (GetCurrentUserRole() == UserRole.Admin)
            {
                _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();
            }
            else if (existingTask.AssignedUserId == GetCurrentUserId())
            {
                existingTask.Status = task.Status;
                _unitOfWork.Tasks.Update(existingTask);
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
