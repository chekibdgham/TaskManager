using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Data;

namespace TaskManagerTests.UnitTests
{
    public class TaskManagementDbTest
    {
        private DbContextOptions<TaskManagementDB> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<TaskManagementDB>()
                .UseInMemoryDatabase(databaseName: "TaskManagementDB_Test")
                .Options;
        }

        [Fact]
        public void CanAddAndRetrieveTasks()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new TaskManagementDB(options))
            {
                context.Database.EnsureCreated();
            }

            // Act
            using (var context = new TaskManagementDB(options))
            {
                var tasks = context.Tasks.ToList();

                // Assert
                Assert.Equal(3, tasks.Count);
                Assert.Equal("Task 1", tasks[0].Title);
                Assert.Equal("Task 2", tasks[1].Title);
                Assert.Equal("Task 3", tasks[2].Title);
            }
        }

        [Fact]
        public void CanAddAndRetrieveUsers()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new TaskManagementDB(options))
            {
                context.Database.EnsureCreated();
            }

            // Act
            using (var context = new TaskManagementDB(options))
            {
                var users = context.Users.ToList();

                // Assert
                Assert.Equal(2, users.Count);
                Assert.Equal("User1", users[0].Username);
                Assert.Equal("User2", users[1].Username);
            }
        }
    }
}
