using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Repositories;
using AJKAccessControl.Tests.Providers;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AJKAccessControl.Tests;
public class UserRepositoryTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        var store = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(
            store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        _userRepository = new UserRepository(_userManagerMock.Object);
    }

    [Fact]
    public async Task GetUserByUserNameAsync_UserExists_ReturnsUser()
    {
        // Arrange
        var userName = "testuser";
        var user = new User { UserName = userName };

        // Mock UserManager.FindByNameAsync
        _userManagerMock.Setup(um => um.FindByNameAsync(userName))
            .ReturnsAsync(user);

        // Mock GetRolesAsync
        _userManagerMock.Setup(um => um.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "User" });

        // Act
        var result = await _userRepository.GetUserByUserNameAsync(userName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userName, result.UserName); // Check the user name
        Assert.Contains("User", result.Roles);  // Check the roles
    }


    [Fact]
    public async Task CreateUserAsync_ValidUser_ReturnsSuccess()
    {
        // Arrange
        var user = new User { UserName = "newuser" };
        var password = "Password123!";
        var role = "User";
        _userManagerMock.Setup(um => um.CreateAsync(user, password))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(um => um.AddToRoleAsync(user, role))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userRepository.CreateUserAsync(user, password, role);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task DeleteUserAsync_UserExists_ReturnsSuccess()
    {
        // Arrange
        var user = new User { UserName = "testuser" };
        _userManagerMock.Setup(um => um.DeleteAsync(user))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userRepository.DeleteUserAsync(user);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task CheckPasswordAsync_CorrectPassword_ReturnsSuccess()
    {
        // Arrange
        var user = new User { UserName = "testuser" };
        var password = "Password123!";
        _userManagerMock.Setup(um => um.CheckPasswordAsync(user, password))
            .ReturnsAsync(true);

        // Act
        var result = await _userRepository.CheckPasswordAsync(user, password);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task UpdateUserAsync_UserExists_ReturnsSuccess()
    {
        // Arrange
        var userName = "testuser";
        var user = new User
        {
            UserName = userName,
            FirstName = "First",
            LastName = "Last",
            Email = "test@example.com",
            Roles = new List<string> { "User" }!
        };
        var existingUser = new User { UserName = userName };
        _userManagerMock.Setup(um => um.FindByNameAsync(user.UserName))
            .ReturnsAsync(existingUser);
        _userManagerMock.Setup(um => um.UpdateAsync(existingUser))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userRepository.UpdateUserAsync(userName, user);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task AddUserToRoleAsync_UserExists_ReturnsSuccess()
    {
        // Arrange
        var userName = "testuser";
        var role = "Admin";
        var user = new User { UserName = userName };
        _userManagerMock.Setup(um => um.FindByNameAsync(userName))
            .ReturnsAsync(user);
        _userManagerMock.Setup(um => um.AddToRoleAsync(user, role))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userRepository.AddUserToRoleAsync(userName, role);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task GetUsersAsync_ReturnsUserList()
    {
        // Arrange
        var users = new List<User>
        {
            new User { UserName = "user1" },
            new User { UserName = "user2" }
        }.AsQueryable();

        // Mockowanie IQueryable<User>
        var mockUsers = new Mock<IQueryable<User>>();
        mockUsers.As<IAsyncEnumerable<User>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<User>(users.GetEnumerator()));

        mockUsers.As<IQueryable<User>>()
            .Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<User>(users.Provider));

        mockUsers.As<IQueryable<User>>()
            .Setup(m => m.Expression)
            .Returns(users.Expression);

        mockUsers.As<IQueryable<User>>()
            .Setup(m => m.ElementType)
            .Returns(users.ElementType);

        mockUsers.As<IQueryable<User>>()
            .Setup(m => m.GetEnumerator())
            .Returns(users.GetEnumerator());

        // Ustawienie UserManager.Users
        _userManagerMock.Setup(um => um.Users)
            .Returns(mockUsers.Object);

        // Act
        var result = await _userRepository.GetUsersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.UserName == "user1");
        Assert.Contains(result, u => u.UserName == "user2");
    }


    [Fact]
    public async Task ChangePasswordAsync_UserExists_ReturnsSuccess()
    {
        // Arrange
        var user = new User { UserName = "testuser" };
        var newPassword = "NewPassword123!";
        _userManagerMock.Setup(um => um.RemovePasswordAsync(user))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(um => um.AddPasswordAsync(user, newPassword))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userRepository.ChangePasswordAsync(user, newPassword);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
    }
}
