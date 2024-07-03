using AutoMapper;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FlexiSourceIT.FlexMarathon.Application.Services;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Application;

public class ActivityServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<ActivityService>> _loggerMock;
    private readonly Mock<IActivityRepository> _activityRepositoryMock;
    private readonly ActivityService _activityService;

    public ActivityServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<ActivityService>>();
        _activityRepositoryMock = new Mock<IActivityRepository>();
        _activityService = new ActivityService(_mapperMock.Object, _loggerMock.Object, _activityRepositoryMock.Object);
    }

    [Fact]
    public async Task AddActivityAsync_ShouldAddActivityAndReturnModel()
    {
        // Arrange
        var activityModel = new ActivityModel
        {
            Id = Guid.NewGuid(),
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1),
        };
        var activity = new Activity
        {
            Id = activityModel.Id,
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1),
            UserProfileId = Guid.NewGuid(),
        };

        _mapperMock.Setup(m => m.Map<Activity>(activityModel)).Returns(activity);

        // Act
        var result = await _activityService.AddActivityAsync(activityModel);

        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(activityModel.Id);

        _activityRepositoryMock.Verify(r => r.AddAsync(activity), Times.Once);
        _activityRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateActivityAsync_ShouldUpdateActivity()
    {
        // Arrange
        var activityModel = new ActivityModel
        {
            Id = Guid.NewGuid(),
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };
        var activity = new Activity
        {
            Id = activityModel.Id,
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1),
            UserProfileId = Guid.NewGuid(),
        };

        _mapperMock.Setup(m => m.Map<Activity>(activityModel)).Returns(activity);

        // Act
        await _activityService.UpdateActivityAsync(activityModel);

        // Assert
        _activityRepositoryMock.Verify(r => r.UpdateAsync(activity), Times.Once);
        _activityRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task GetActivitiesAsync_ShouldReturnActivities()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var activities = new List<Activity>
        {
            new() { Id = Guid.NewGuid(),
                UserProfileId = userProfileId,
                Location = "People's Park",
                DateTimeStarted = new DateTime(2024, 1, 1),
            },
            new() { Id = Guid.NewGuid(),
                UserProfileId = userProfileId,
                Location = "People's Park",
                DateTimeStarted = new DateTime(2024, 1, 1)
            }
        };

        _activityRepositoryMock.Setup(r => r.GetListAsync(It.IsAny<Expression<Func<Activity, bool>>>()))
                               .ReturnsAsync(activities);

        var activityModels = new List<ActivityModel>
        {
            new() { Id = activities[0].Id,
                                Location = "People's Park",
                                DateTimeStarted = new DateTime(2024, 1, 1)
            },
            new() { Id = activities[1].Id,
                                Location = "Talomo Beach",
                                DateTimeStarted = new DateTime(2024, 1, 1),
            }
        };

        _mapperMock.Setup(m => m.Map<List<ActivityModel>>(activities)).Returns(activityModels);

        // Act
        var result = await _activityService.GetActivitiesAsync(userProfileId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);

        _activityRepositoryMock.Verify(r => r.GetListAsync(It.IsAny<Expression<Func<Activity, bool>>>()), Times.Once);
    }


    [Fact]
    public async Task GetActivityAsync_ShouldReturnActivity()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        var activity = new Activity
        {
            Id = activityId,
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1),
            UserProfileId = Guid.NewGuid(),
        };

        _activityRepositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<Activity, bool>>>()))
                               .ReturnsAsync(activity);

        var activityModel = new ActivityModel
        {
            Id = activityId,
            Location = "People's Park",
            DateTimeStarted = new DateTime(2024, 1, 1)
        };

        _mapperMock.Setup(m => m.Map<ActivityModel>(activity)).Returns(activityModel);

        // Act
        var result = await _activityService.GetActivityAsync(activityId);

        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(activityId);

        _activityRepositoryMock.Verify(r => r.GetSingleAsync(It.IsAny<Expression<Func<Activity, bool>>>()), Times.Once);
    }
}