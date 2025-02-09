using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagerTests.UnitTests;
public class UserRepositoryTests
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

    private Mock<IValidator<DtoUser>> CreateValidatorMock()
    {
        var validatorMock = new Mock<IValidator<DtoUser>>();
        validatorMock.Setup(v => v.Validate(It.IsAny<DtoUser>())).Returns(new ValidationResult());
        return validatorMock;
    }

    [Fact]
    public void GetAll_ShouldReturnAllUsers()
    {
        // Arrange
        using var context = CreateInMemoryDatabase();
        var validatorMock = CreateValidatorMock();
        var repository = new UserRepository(context, validatorMock.Object);

        // Act
        var users = repository.GetAll();

        // Assert
        Assert.Equal(2, users.Count());
    }

    [Fact]
    public void GetById_ShouldReturnUser()
    {
        // Arrange
        using var context = CreateInMemoryDatabase();
        var validatorMock = CreateValidatorMock();
        var repository = new UserRepository(context, validatorMock.Object);

        // Act
        var user = repository.GetById(2);

        // Assert
        Assert.NotNull(user);
        Assert.Equal("user2", user.UserName);
    }

    [Fact]
    public void GetByName_ShouldReturnUser()
    {
        // Arrange
        using var context = CreateInMemoryDatabase();
        var validatorMock = CreateValidatorMock();
        var repository = new UserRepository(context, validatorMock.Object);

        // Act
        var user = repository.GetByName("admin");

        // Assert
        Assert.NotNull(user);
        Assert.Equal("admin", user.Username);
    }

    //[Fact]
    //public void Add_ShouldAddUser()
    //{
    //    // Arrange
    //    using var context = CreateInMemoryDatabase();
    //    var validatorMock = CreateValidatorMock();
    //    var repository = new UserRepository(context, validatorMock.Object);
    //    var newUser = new DtoUser { UserId = 3, UserName = "newuser", Role = "User" };

    //    // Act
    //    repository.Add(newUser);

    //    // Assert
    //    var user = context.Users.FirstOrDefault(u => u.Username == "newuser");
    //    Assert.NotNull(user);
    //    Assert.Equal("newuser", user.Username);
    //}

    //[Fact]
    //public void Update_ShouldUpdateUser()
    //{
    //    // Arrange
    //    using var context = CreateInMemoryDatabase();
    //    var validatorMock = CreateValidatorMock();
    //    var repository = new UserRepository(context, validatorMock.Object);
    //    var updatedUser = new DtoUser { UserId = 1, UserName = "updatedadmin", Role = "Admin" };

    //    // Act
    //    repository.Update(updatedUser);

    //    // Assert
    //    var user = context.Users.FirstOrDefault(u => u.Id == 1);
    //    Assert.NotNull(user);
    //    Assert.Equal("updatedadmin", user.Username);
    //}

    //[Fact]
    //public void Delete_ShouldRemoveUser()
    //{
    //    // Arrange
    //    using var context = CreateInMemoryDatabase();
    //    var validatorMock = CreateValidatorMock();
    //    var repository = new UserRepository(context, validatorMock.Object);

    //    // Act
    //    repository.Delete(1);

    //    // Assert
    //    var user = context.Users.FirstOrDefault(u => u.Id == 1);
    //    Assert.Null(user);
    //}
}
