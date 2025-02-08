using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByName(string name);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
