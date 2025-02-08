using System;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Repositories.Interfaces;


namespace TaskManagementAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagementDB _context;

        public UnitOfWork(TaskManagementDB context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Tasks = new TaskToDoRepository(_context);
        }

        public IUserRepository Users { get; }
        public ITaskToDoRepository Tasks { get; }


        public int Save() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
