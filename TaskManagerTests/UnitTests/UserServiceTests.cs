using Moq;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using TaskManagementAPI.Repositories.Interfaces;

namespace TaskManagementAPI.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepository.Object);
            _userService = new UserService(_mockUnitOfWork.Object);
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
            var result = _userService.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("User1", result.First().Username);
        }

        [Fact]
        public void GetById_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Role = UserRole.Admin };
            _mockUserRepository.Setup(repo => repo.GetById(1)).Returns(user);

            // Act
            var result = _userService.GetById(1);

            // Assert
            Assert.Equal("User1", result.Username);
        }

        [Fact]
        public void Add_ShouldAddUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Role = UserRole.Admin };

            // Act
            _userService.Add(user);

            // Assert
            _mockUserRepository.Verify(repo => repo.Add(user), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Role = UserRole.Admin };

            // Act
            _userService.Update(user);

            // Assert
            _mockUserRepository.Verify(repo => repo.Update(user), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public void Delete_ShouldDeleteUser()
        {
            // Arrange
            var userId = 1;

            // Act
            _userService.Delete(userId);

            // Assert
            _mockUserRepository.Verify(repo => repo.Delete(userId), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }
    }
}

