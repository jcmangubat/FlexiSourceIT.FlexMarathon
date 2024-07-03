using FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Repositories;
using FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures;
using FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure;

public class ActivityRepositoryTests : IClassFixture<ActivityRepositoryTestsFixture>
{
    private readonly IActivityRepository _activityRepository;
    private readonly RepositoryTestsFixture<ActivityRepository, Activity> _fixture;

    public ActivityRepositoryTests(ActivityRepositoryTestsFixture fixture)
    {
        _fixture = fixture.Fixture;
        _activityRepository = _fixture.Repository;
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        var activity = new Activity
        {
            Location = "Park",
            DateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 1, 11, 0, 0),
            Distance = 5.0,
            Duration = TimeSpan.FromHours(1),
            AveragePace = 6.0,
            UserProfileId = Guid.NewGuid()
        };

        // Act
        await _activityRepository.AddAsync(activity);
        await _activityRepository.CommitAsync();

        // Assert
        var addedActivity = await _fixture.DbContext.Activities.FirstOrDefaultAsync(a => a.Location == "Park");
        addedActivity.Should().NotBeNull();
        addedActivity?.Location.Should().Be("Park");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEntity()
    {
        // Arrange
        var activity = new Activity
        {
            Location = "Park 2",
            DateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 1, 11, 0, 0),
            Distance = 5.0,
            Duration = TimeSpan.FromHours(1),
            AveragePace = 6.0,
            UserProfileId = Guid.NewGuid()
        };

        await _activityRepository.AddAsync(activity);
        await _activityRepository.CommitAsync();

        // Act
        await _activityRepository.DeleteAsync(activity);
        await _activityRepository.CommitAsync();

        // Assert
        var deletedActivity = await _fixture.DbContext.Activities.FirstOrDefaultAsync(a => a.Location == "Park 2");
        deletedActivity.Should().BeNull();
    }

    [Fact]
    public async Task GetSingleAsync_ShouldReturnActivity()
    {
        // Arrange
        var activity = new Activity
        {
            Location = "Park",
            DateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 1, 11, 0, 0),
            Distance = 5.0,
            Duration = TimeSpan.FromHours(1),
            AveragePace = 6.0,
            UserProfileId = Guid.NewGuid()
        };

        await _activityRepository.AddAsync(activity);
        await _activityRepository.CommitAsync();

        // Act
        var fetchedActivity = await _activityRepository.GetSingleAsync(a => a.Location == "Park");

        // Assert
        fetchedActivity.Should().NotBeNull();
        fetchedActivity?.Location.Should().Be("Park");
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnActivities()
    {
        // Arrange
        var activity1 = new Activity
        {
            Location = "Park 3",
            DateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 1, 11, 0, 0),
            Distance = 5.0,
            Duration = TimeSpan.FromHours(1),
            AveragePace = 6.0,
            UserProfileId = Guid.NewGuid()
        };

        var activity2 = new Activity
        {
            Location = "Beach Head",
            DateTimeStarted = new DateTime(2024, 1, 2, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 2, 11, 0, 0),
            Distance = 3.0,
            Duration = TimeSpan.FromHours(0.5),
            AveragePace = 10.0,
            UserProfileId = Guid.NewGuid()
        };

        await _activityRepository.AddAsync(activity1, activity2);
        await _activityRepository.CommitAsync();

        // Act
        var activities = await _activityRepository.GetListAsync(a => a.Distance > 0);

        // Assert
        //activities.Should().HaveCount(2);
        activities.Should().Contain(a => a.Location == "Park 3");
        activities.Should().Contain(a => a.Location == "Beach Head");
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyEntity()
    {
        // Arrange
        var activity = new Activity
        {
            Location = "Park",
            DateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 1, 11, 0, 0),
            Distance = 5.0,
            Duration = TimeSpan.FromHours(1),
            AveragePace = 6.0,
            UserProfileId = Guid.NewGuid()
        };

        await _activityRepository.AddAsync(activity);
        await _activityRepository.CommitAsync();

        // Act
        activity.Location = "Updated Park";
        await _activityRepository.UpdateAsync(activity);
        await _activityRepository.CommitAsync();

        // Assert
        var updatedActivity = await _fixture.DbContext.Activities.FirstOrDefaultAsync(a => a.Location == "Updated Park");
        updatedActivity.Should().NotBeNull();
        updatedActivity?.Location.Should().Be("Updated Park");
    }

    [Fact]
    public async Task GetSingleAsync_WithFilter_ShouldReturnFilteredActivity()
    {
        // Arrange
        var activity1 = new Activity
        {
            Location = "Park",
            DateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 1, 11, 0, 0),
            Distance = 5.0,
            Duration = TimeSpan.FromHours(1),
            AveragePace = 6.0,
            UserProfileId = Guid.NewGuid()
        };

        var activity2 = new Activity
        {
            Location = "Beach",
            DateTimeStarted = new DateTime(2024, 1, 2, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 2, 11, 0, 0),
            Distance = 3.0,
            Duration = TimeSpan.FromHours(0.5),
            AveragePace = 10.0,
            UserProfileId = Guid.NewGuid()
        };

        await _activityRepository.AddAsync(activity1, activity2);
        await _activityRepository.CommitAsync();

        // Act
        var fetchedActivity = await _activityRepository.GetSingleAsync(a => a.Location == "Beach");

        // Assert
        fetchedActivity.Should().NotBeNull();
        fetchedActivity?.Location.Should().Be("Beach");
    }

    [Fact]
    public async Task GetListAsync_WithFilter_ShouldReturnFilteredActivities()
    {
        // Arrange
        var activity1 = new Activity
        {
            Location = "Park Hour",
            DateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 1, 11, 0, 0),
            Distance = 5.0,
            Duration = TimeSpan.FromHours(1),
            AveragePace = 6.0,
            UserProfileId = Guid.NewGuid()
        };

        var activity2 = new Activity
        {
            Location = "Beach Neds",
            DateTimeStarted = new DateTime(2024, 1, 2, 10, 0, 0),
            DateTimeEnded = new DateTime(2024, 1, 2, 11, 0, 0),
            Distance = 3.0,
            Duration = TimeSpan.FromHours(0.5),
            AveragePace = 10.0,
            UserProfileId = Guid.NewGuid()
        };

        await _activityRepository.AddAsync(activity1, activity2);
        await _activityRepository.CommitAsync();

        var filter = new ActivityModel
        {
            Location = "Beach Neds",
            DateTimeStarted = new DateTime(2024, 1, 2)
        };

        // Act
        var activities = await _activityRepository.GetListAsync(a => a.Location == filter.Location);

        // Assert
        activities.Should().HaveCount(1);
        activities.First().Location.Should().Be("Beach Neds");
    }
}
