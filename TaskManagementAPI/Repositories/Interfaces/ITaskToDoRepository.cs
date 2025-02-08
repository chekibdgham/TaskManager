using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories.Interfaces
{
    public interface ITaskToDoRepository
    {
        IEnumerable<TaskToDo> GetAll();
        TaskToDo GetById(int id);
        void Add(TaskToDo task);
        IEnumerable<TaskToDo> GetByUserId(int userId);
        void Update(TaskToDo task);
        void Delete(int id);
    }
}
