﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories.Interfaces;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class TaskToDoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskToDoRepository _taskToDoRepository;
        private readonly IIdentityService _identityService;

        public TaskToDoService(IUnitOfWork unitOfWork,ITaskToDoRepository taskToDoRepository,IIdentityService identityService  )
        {
            _unitOfWork = unitOfWork;
            _taskToDoRepository = taskToDoRepository;
            _identityService = identityService;
        }

        

        public async Task<List<TaskToDo>> GetAll(CancellationToken cancellationToken)
        {
            if (_identityService.GetCurrentUserRole() == UserRole.Admin)
            {
                return  await _taskToDoRepository.GetAllAsync(cancellationToken);
            }
            throw new UnauthorizedAccessException("Only Admin can view all tasks.");
        }

        public async Task<TaskToDo> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var task = await _taskToDoRepository.GetByIdAsync(id, cancellationToken);
            if (_identityService.GetCurrentUserRole() == UserRole.Admin || task.AssignedUserId == _identityService.GetCurrentUserId())
            {
                return task;
            }
            throw new UnauthorizedAccessException("You can only view tasks assigned to you.");
        }

        public async Task Add(TaskToDo task,CancellationToken cancellationToken)
        {
            if (_identityService.GetCurrentUserRole() == UserRole.Admin)
            {
                await _taskToDoRepository.AddAsync(task, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new UnauthorizedAccessException("Only Admin can create tasks.");
            }
        }

        public async Task Update(TaskToDo task, CancellationToken cancellationToken)
        {
            var existingTask = await _taskToDoRepository.GetByIdAsync(task.Id);

            if (existingTask is null)
            {
                throw new ArgumentException("Task not found", nameof(task.Id));
            }

            if (_identityService.GetCurrentUserRole() == UserRole.Admin)
            {
                existingTask.Update(task);
                _taskToDoRepository.Update(task);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            
            else
            {
                throw new UnauthorizedAccessException("You can only update the status of your assigned tasks.");
            }
        }
        public async Task UpdateTaskStatus(int id, TStatus newTaskStatus, CancellationToken cancellationToken)
        {
           
            var task = await _taskToDoRepository.GetByIdAsync(id);

            if (task is null)
            {
                throw new ArgumentException("Task not found", nameof(id));
            }
                
            if (_identityService.GetCurrentUserRole() == UserRole.Admin || task.AssignedUserId == _identityService.GetCurrentUserId())
            {
                task.UpdateStatus(newTaskStatus);
                _taskToDoRepository.Update(task);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            
            else
            {
                throw new UnauthorizedAccessException("You can only update the status of your assigned tasks.");
            }
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            if (_identityService.GetCurrentUserRole() == UserRole.Admin)
            {
               
                _taskToDoRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new UnauthorizedAccessException("Only Admin can delete tasks.");
            }
        }

        public async Task<List<TaskToDo>> GetByUserId(int userId, CancellationToken cancellationToken) => await _taskToDoRepository.GetByUserIdAsync(userId,cancellationToken);
    }
}
