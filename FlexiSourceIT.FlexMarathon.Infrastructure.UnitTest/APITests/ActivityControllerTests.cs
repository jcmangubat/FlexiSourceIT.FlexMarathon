using FlexiSourceIT.FlexMarathon.API.Controllers;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.UnitTest.APITests;

public class ActivityControllerTests
{
    private readonly Mock<ILogger<ActivityController>> _loggerMock;
    private readonly Mock<IActivityService> _activityServiceMock;
    private readonly ActivityController _activityController;

    public ActivityControllerTests()
    {
        _loggerMock = new Mock<ILogger<ActivityController>>();
        _activityServiceMock = new Mock<IActivityService>();
        _activityController = new ActivityController(_loggerMock.Object, _activityServiceMock.Object);
    }

    [Fact]
    public async Task AddActivityAsync_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var activityModel = new ActivityModel
        {
            Id = Guid.NewGuid(),
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };

        _activityServiceMock.Setup(s => s.AddActivityAsync(activityModel)).ReturnsAsync(activityModel);

        // Act
        var result = await _activityController.AddActivityAsync(activityModel) as CreatedAtActionResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(activityModel);
        result?.ActionName.Should().Be(nameof(ActivityController.GetActivityAsync));
    }

    [Fact]
    public async Task UpdateActivityAsync_ShouldReturnNoContent()
    {
        // Arrange
        var activityModel = new ActivityModel
        {
            Id = Guid.NewGuid(),
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };

        // Act
        var result = await _activityController.UpdateActivityAsync(activityModel) as NoContentResult;

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetActivityAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        var activityModel = new ActivityModel
        {
            Id = activityId,
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };

        _activityServiceMock.Setup(s => s.GetActivityAsync(activityId)).ReturnsAsync(activityModel);

        // Act
        var result = await _activityController.GetActivityAsync(activityId) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(activityModel);
    }

    [Fact]
    public async Task GetActivitiesAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var activityModels = new List<ActivityModel>
        {
            new() { Id = Guid.NewGuid(), Location = "People's Park", DateTimeStarted = new DateTime(2024, 1, 1) },
            new() { Id = Guid.NewGuid(), Location = "Talomo Beach", DateTimeStarted = new DateTime(2024, 1, 1) }
        };

        _activityServiceMock.Setup(s => s.GetActivitiesAsync(userProfileId)).ReturnsAsync(activityModels);

        // Act
        var result = await _activityController.GetActivitiesAsync(userProfileId) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(activityModels);
    }

    [Fact]
    public async Task GetActivityWithModelAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var activityModel = new ActivityModel
        {
            Id = Guid.NewGuid(),
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };

        _activityServiceMock.Setup(s => s.GetActivityAsync(activityModel)).ReturnsAsync(activityModel);

        // Act
        var result = await _activityController.GetActivityWithModelAsync(activityModel) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(activityModel);
    }

    [Fact]
    public async Task GetActivitiesWithModelAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var activityModel = new ActivityModel
        {
            Id = Guid.NewGuid(),
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };
        var activityModels = new List<ActivityModel>
        {
            new() { Id = Guid.NewGuid(), Location = "People's Park", DateTimeStarted = new DateTime(2024, 1, 1) },
            new() { Id = Guid.NewGuid(), Location = "Talomo Beach", DateTimeStarted = new DateTime(2024, 1, 1) }
        };

        _activityServiceMock.Setup(s => s.GetActivitiesAsync(activityModel)).ReturnsAsync(activityModels);

        // Act
        var result = await _activityController.GetActivitiesWithModelAsync(activityModel) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(activityModels);
    }

    [Fact]
    public async Task GetActivityByExpressionAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var filterExpression = "Location == \"People's Park\"";
        var activityModel = new ActivityModel
        {
            Id = Guid.NewGuid(),
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };

        _activityServiceMock.Setup(s => s.GetActivityAsync(It.IsAny<Expression<Func<ActivityModel, bool>>>())).ReturnsAsync(activityModel);

        // Act
        var result = await _activityController.GetActivityByExpressionAsync(filterExpression) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(activityModel);
    }

    [Fact]
    public async Task GetActivitiesByFilterAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var filterExpression = "Location == \"People's Park\"";
        var activityModels = new List<ActivityModel>
        {
            new() { Id = Guid.NewGuid(), Location = "People's Park", DateTimeStarted = new DateTime(2024, 1, 1) },
            new() { Id = Guid.NewGuid(), Location = "Talomo Beach", DateTimeStarted = new DateTime(2024, 1, 1) }
        };

        _activityServiceMock.Setup(s => s.GetActivitiesByFilterAsync(It.IsAny<Expression<Func<ActivityModel, bool>>>())).ReturnsAsync(activityModels);

        // Act
        var result = await _activityController.GetActivitiesByFilterAsync(filterExpression) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().Be(activityModels);
    }
}
