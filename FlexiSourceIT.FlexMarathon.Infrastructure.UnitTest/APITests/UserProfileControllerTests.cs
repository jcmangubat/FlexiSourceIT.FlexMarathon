using FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.API.Controllers;

public class UserProfileControllerTests
{
    private readonly Mock<ILogger<UserProfileController>> _loggerMock;
    private readonly Mock<IUserProfileService> _userProfileServiceMock;
    private readonly UserProfileController _userProfileController;

    public UserProfileControllerTests()
    {
        _loggerMock = new Mock<ILogger<UserProfileController>>();
        _userProfileServiceMock = new Mock<IUserProfileService>();
        _userProfileController = new UserProfileController(_loggerMock.Object, _userProfileServiceMock.Object);
    }

    [Fact]
    public async Task AddUserProfileAsync_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var userProfileModel = new UserProfileModel
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 7, 3),
            Height = 133,
            Weight = 70
        };

        _userProfileServiceMock.Setup(s => s.AddUserProfileAsync(userProfileModel)).ReturnsAsync(userProfileModel);

        // Act
        var result = await _userProfileController.AddUserProfileAsync(userProfileModel) as CreatedAtActionResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(userProfileModel);
        result?.ActionName.Should().Be(nameof(UserProfileController.GetUserProfileAsync));
    }

    [Fact]
    public async Task UpdateUserProfileAsync_ShouldReturnNoContent()
    {
        // Arrange
        var userProfileModel = new UserProfileModel
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 7, 3),
            Height = 133,
            Weight = 70
        };

        // Act
        var result = await _userProfileController.UpdateUserProfileAsync(userProfileModel) as NoContentResult;

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUserProfileAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var userProfileModel = new UserProfileModel
        {
            Id = userProfileId,
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 7, 3),
            Height = 133,
            Weight = 70
        };

        _userProfileServiceMock.Setup(s => s.GetUserProfileAsync(userProfileId)).ReturnsAsync(userProfileModel);

        // Act
        var result = await _userProfileController.GetUserProfileAsync(userProfileId) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(userProfileModel);
    }

    [Fact]
    public async Task GetUserProfileAsync_ShouldReturnBadRequestForInvalidFilterExpression()
    {
        // Arrange
        var filterExpression = "invalid filter expression";

        // Act
        var result = await _userProfileController.GetUserProfileAsync(filterExpression) as BadRequestObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be("Invalid filter expression.");
    }

    [Fact]
    public async Task GetUserProfileAsync_ShouldReturnOkObjectResultForValidFilterExpression()
    {
        // Arrange
        var filterExpression = "Name==\"Juan dela Cruz\"";
        var userProfileModel = new UserProfileModel
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 7, 3),
            Height = 133,
            Weight = 70
        };

        _userProfileServiceMock.Setup(s => s.GetUserProfileAsync(It.IsAny<Expression<Func<UserProfileModel, bool>>>())).ReturnsAsync(userProfileModel);

        // Act
        var result = await _userProfileController.GetUserProfileAsync(filterExpression) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(userProfileModel);
    }

    [Fact]
    public async Task GetUserProfilesAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var filterExpression = "Name == \"Juan dela Cruz\"";
        var userProfileModels = new List<UserProfileModel>
        {
            new() { 
                Id = Guid.NewGuid(), 
                Name = "Juan dela Cruz",
                BirthDate = new DateTime(1980, 7, 3),
                Height = 133,
                Weight = 70
            },
            new() { 
                Id = Guid.NewGuid(), 
                Name = "Timmy Largo",
                BirthDate = new DateTime(1980, 7, 3),
                Height = 133,
                Weight = 70
            }
        };

        _userProfileServiceMock.Setup(s => s.GetUserProfilesAsync(It.IsAny<Expression<Func<UserProfileModel, bool>>>())).ReturnsAsync(userProfileModels);

        // Act
        var result = await _userProfileController.GetUserProfilesAsync(filterExpression) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(userProfileModels);
    }

    [Fact]
    public async Task GetUserProfilesAsync_ShouldReturnBadRequestForInvalidFilterExpression()
    {
        // Arrange
        var filterExpression = "invalid filter expression";

        // Act
        var result = await _userProfileController.GetUserProfilesAsync(filterExpression) as BadRequestObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be("Invalid filter expression.");
    }
}
