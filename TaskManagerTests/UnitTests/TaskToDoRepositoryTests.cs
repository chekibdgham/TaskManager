using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models.TaskToDo;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Repositories.Interfaces;
using Xunit;

namespace TaskManagerTests.UnitTests;
public class TaskToDoRepositoryTests
{
    private TaskManagementDB CreateInMemoryDatabase()
    {
        var options = new DbContextOptionsBuilder<TaskManagementDB>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new TaskManagementDB(options);
        context.Database.EnsureCreated();
        return context;
    }


    [Fact]
    public async Task GetByIdAsync_ShouldReturnTask()
    {
        // Arrange
        using var context = CreateInMemoryDatabase();
        var repository = new TaskToDoRepository(context);
        // Act
        var task = await repository.GetByIdAsync(1);
        // Assert
        Assert.NotNull(task);
        Assert.Equal("Task 1", task.Title);
        Assert.Equal("Description 1", task.Description);
        Assert.Equal(TStatus.Open, task.Status);
        Assert.Equal(1, task.AssignedUserId);
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnTasks()
    {
        // Arrange
        using var context = CreateInMemoryDatabase();
        var repository = new TaskToDoRepository(context);
        // Act
        var tasks = await repository.GetByUserIdAsync(1);
        // Assert
        Assert.Equal(2, tasks.Count);
    }
}

