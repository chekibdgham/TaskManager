using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using Xunit;

namespace TaskManagerTests.IntegrationTests;
public class TaskManagementDBTests
{
    private TaskManagementDB CreateInMemoryDatabase()
    {
        var options = new DbContextOptionsBuilder<TaskManagementDB>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new TaskManagementDB(options);
    }

    [Fact]
    public void SeedDatabase_ShouldAddSeedData()
    {
        // Arrange
        using var context = CreateInMemoryDatabase();

        // Act
        context.Database.EnsureCreated();

        // Assert
        Assert.Equal(2, context.Users.Count());
        Assert.Equal(3, context.Tasks.Count());

        var adminUser = context.Users.FirstOrDefault(u => u.Username == "admin");
        var normalUser = context.Users.FirstOrDefault(u => u.Username == "user2");

        Assert.NotNull(adminUser);
        Assert.NotNull(normalUser);
        Assert.Equal(UserRole.Admin, adminUser.Role);
        Assert.Equal(UserRole.User, normalUser.Role);

        var task1 = context.Tasks.FirstOrDefault(t => t.Title == "Task 1");
        var task2 = context.Tasks.FirstOrDefault(t => t.Title == "Task 2");
        var task3 = context.Tasks.FirstOrDefault(t => t.Title == "Task 3");

        Assert.NotNull(task1);
        Assert.NotNull(task2);
        Assert.NotNull(task3);
        Assert.Equal(TStatus.Open, task1.Status);
        Assert.Equal(TStatus.InProgress, task2.Status);
        Assert.Equal(TStatus.Closed, task3.Status);
    }
}
