using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User? ValidateUserAsync(LoginRequest loginRequest)
        {
            var user = _unitOfWork.Users
                .GetAll()
                .FirstOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
                 
            return user;
        }

        public IEnumerable<User> GetAll() => _unitOfWork.Users.GetAll();
        public User GetById(int id) => _unitOfWork.Users.GetById(id);
        public void Add(User user) { _unitOfWork.Users.Add(user); _unitOfWork.Save(); }
        public void Update(User user) { _unitOfWork.Users.Update(user); _unitOfWork.Save(); }
        public void Delete(int id) { _unitOfWork.Users.Delete(id); _unitOfWork.Save(); }
    }
}
