using TaskManagementAPI.Repositories.Interfaces;

namespace TaskManagementAPI.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskToDoRepository Tasks { get; }
        int Save();
    }
}
