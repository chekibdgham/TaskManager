using System;
using TaskManagementAPI.Models;
using TaskManagementAPI.Data;
using TaskManagementAPI.Repositories.Interfaces;

namespace TaskManagementAPI.Repositories
{
    public class UserRepository(TaskManagementDB context) : IUserRepository
    {
        private readonly TaskManagementDB _context = context;

       

        public IEnumerable<User> GetAll() => _context.Users.ToList();
        public User GetById(int id)
        {
            if (id <= 0) throw new ArgumentException("Id must be greater than zero", nameof(id));
            var user = _context.Users.Find(id);
            return user ?? throw new ArgumentException("User not found", nameof(id));
        }
        public User GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name must not be null or empty", nameof(name));
            var user = _context.Users.FirstOrDefault(u => u.Username == name);
            return user ?? throw new ArgumentException("User not found", nameof(name));
        }
        public void Add(User user) { _context.Users.Add(user); _context.SaveChanges(); }
        public void Update(User user) { _context.Users.Update(user); _context.SaveChanges(); }
        public void Delete(int id) { var user = _context.Users.Find(id); if (user != null) { _context.Users.Remove(user); _context.SaveChanges(); } }
    }
}
