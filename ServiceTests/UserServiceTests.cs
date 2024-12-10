using Moq;
using Microsoft.AspNetCore.Identity;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data;

namespace ServiceTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            // Mock UserManager<ApplicationUser>
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            _userService = new UserService(_mockUserManager.Object);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = Guid.NewGuid(), Email = "user1@example.com" },
                new ApplicationUser { Id = Guid.NewGuid(), Email = "user2@example.com" }
            };

            var roles1 = new List<string> { "Admin" };
            var roles2 = new List<string> { "User" };

            _mockUserManager.Setup(um => um.Users).Returns(users.AsQueryable());
            _mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                            .ReturnsAsync((ApplicationUser user) => user.Email == "user1@example.com" ? roles1 : roles2);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            var user1 = result.First();
            var user2 = result.Last();

            Assert.That(user1.Email, Is.EqualTo("user1@example.com"));
            Assert.Contains("Admin", user1.Roles.ToList());

            Assert.That(user2.Email, Is.EqualTo("user2@example.com"));
            Assert.Contains("User", user2.Roles.ToList());
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<ApplicationUser>();

            _mockUserManager.Setup(um => um.Users).Returns(users.AsQueryable());

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldIncludeRoles()
        {
            // Arrange
            var user = new ApplicationUser { Id = Guid.NewGuid(), Email = "user@example.com" };
            var roles = new List<string> { "Admin", "User" };

            _mockUserManager.Setup(um => um.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());
            _mockUserManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            var userViewModel = result.First();
            Assert.That(userViewModel.Email, Is.EqualTo("user@example.com"));
            Assert.Contains("Admin", userViewModel.Roles.ToList());
            Assert.Contains("User", userViewModel.Roles.ToList());
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldHandleNullRolesGracefully()
        {
            // Arrange
            var user = new ApplicationUser { Id = Guid.NewGuid(), Email = "user@example.com" };

            // Setup the mock to return null for roles
            _mockUserManager.Setup(um => um.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());
            _mockUserManager.Setup(um => um.GetRolesAsync(user))!.ReturnsAsync((IList<string>?)null);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            var userViewModel = result.First();
            Assert.That(userViewModel.Email, Is.EqualTo("user@example.com"));

            Assert.IsEmpty(userViewModel.Roles ?? new List<string>());
        }
    }
}
