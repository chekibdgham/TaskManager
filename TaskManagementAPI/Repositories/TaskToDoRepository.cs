using System;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories.Interfaces;

namespace TaskManagementAPI.Repositories
{
    public class TaskToDoRepository(TaskManagementDB context):ITaskToDoRepository
    {
        private readonly TaskManagementDB _context = context;

        public IEnumerable<TaskToDo> GetAll() => _context.Tasks.ToList();

        public TaskToDo GetById(int id)
        {
            if (id <= 0) throw new ArgumentException("Id must be greater than zero", nameof(id));
            var taskToDo = _context.Tasks.Find(id);
            return taskToDo ?? throw new ArgumentException("Task not found", nameof(id));
        }

        public IEnumerable<TaskToDo> GetByUserId(int userId) => _context.Tasks.Where(t => t.AssignedUserId == userId).ToList();

        public void Add(TaskToDo task) { _context.Tasks.Add(task); }

        public void Update(TaskToDo task) { _context.Tasks.Update(task); }

        public void Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null) _context.Tasks.Remove(task);
        }
    }
}
