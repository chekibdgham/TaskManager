using Microsoft.EntityFrameworkCore;
using System;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data;

public class TaskManagementDB : DbContext
{
    public DbSet<TaskToDo> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public TaskManagementDB(DbContextOptions<TaskManagementDB> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskToDo>().HasData(
            new TaskToDo { Id = 1, Title = "Task 1", Description = "Description 1", Status = TStatus.Open, AssignedUserId = 1 },
            new TaskToDo { Id = 2, Title = "Task 2", Description = "Description 2", Status = TStatus.InProgress, AssignedUserId = 2 },
            new TaskToDo { Id = 3, Title = "Task 3", Description = "Description 3", Status = TStatus.Closed, AssignedUserId = 1 }
        );
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "User1", Role = UserRole.Admin },
            new User { Id = 2, Username = "User2", Role = UserRole.User }
        );
    }
}

