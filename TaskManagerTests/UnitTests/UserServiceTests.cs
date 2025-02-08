using Moq;
using TaskManagementAPI.Controllers;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementAPI.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UsersController _usersController;

        public UsersControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _usersController = new UsersController(_mockUserRepository.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "User1", Role = UserRole.Admin },
                new User { Id = 2, Username = "User2", Role = UserRole.User }
            };
            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(users);

            // Act
            var result = _usersController.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var returnedUsers = result.Value as IEnumerable<User>;
            Assert.Equal(2, returnedUsers.Count());
            Assert.Equal("User1", returnedUsers.First().Username);
        }

        [Fact]
        public void GetById_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Role = UserRole.Admin };
            _mockUserRepository.Setup(repo => repo.GetById(1)).Returns(user);

            // Act
            var result = _usersController.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var returnedUser = result.Value as User;
            Assert.Equal("User1", returnedUser.Username);
        }

        [Fact]
        public void Create_ShouldAddUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Role = UserRole.Admin };

            // Act
            var result = _usersController.Create(user) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_usersController.GetById), result.ActionName);
            Assert.Equal(user.Id, result.RouteValues["id"]);
            Assert.Equal(user, result.Value);
            _mockUserRepository.Verify(repo => repo.Add(user), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Role = UserRole.Admin };

            // Act
            var result = _usersController.Update(1, user) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            _mockUserRepository.Verify(repo => repo.Update(user), Times.Once);
        }

        [Fact]
        public void Delete_ShouldDeleteUser()
        {
            // Arrange
            var userId = 1;

            // Act
            var result = _usersController.Delete(userId) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            _mockUserRepository.Verify(repo => repo.Delete(userId), Times.Once);
        }
    }
}
