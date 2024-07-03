using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FluentAssertions;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Domain.Entities;

public class ActivityTests
{
    private readonly DateTime _dateTimeStarted = new(2024, 1, 1, 10, 0, 0);
    private readonly DateTime _dateTimeEnded = new(2024, 1, 1, 11, 0, 0);
    private readonly double _distance = 5.0;
    private readonly TimeSpan _duration = TimeSpan.FromHours(1);
    private readonly double _averagePace = 6.0;
    private readonly string _location = "Davao People's Park";
    private readonly UserProfile _userProfile = new()
    {
        Id = Guid.NewGuid(),
        Name = "Joan Raneses",
        BirthDate = new DateTime(1986, 3, 1),
        Height = 160.02,
        Weight = 65
    };

    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        // Act
        var activity = new Activity
        {
            Location = _location,
            DateTimeStarted = _dateTimeStarted,
            DateTimeEnded = _dateTimeEnded,
            Distance = _distance,
            Duration = _duration,
            AveragePace = _averagePace,
            UserProfileId = _userProfile.Id,
            UserProfile = _userProfile
        };

        // Assert
        activity.Location.Should().Be(_location);
        activity.DateTimeStarted.Should().Be(_dateTimeStarted);
        activity.DateTimeEnded.Should().Be(_dateTimeEnded);
        activity.Distance.Should().Be(_distance);
        activity.Duration.Should().Be(_duration);
        activity.AveragePace.Should().Be(_averagePace);
        activity.UserProfileId.Should().Be(_userProfile.Id);
        activity.UserProfile.Should().Be(_userProfile);
    }

    [Fact]
    public void Duration_ShouldCalculateCorrectly()
    {
        // Arrange
        // Act
        var activity = new Activity
        {
            DateTimeStarted = _dateTimeStarted,
            DateTimeEnded = _dateTimeEnded,
            Distance = _distance,
            Location = _location,
            UserProfileId = _userProfile.Id,
        };
        var duration = activity.DateTimeEnded - activity.DateTimeStarted;

        // Assert
        duration.Should().Be(TimeSpan.FromHours(1));
    }

    [Fact]
    public void AveragePace_ShouldCalculateCorrectly()
    {
        // Arrange
        // Act
        var averagePace = CalculateAveragePace(_distance, _duration);

        // Assert
        averagePace.Should().Be(0.083333333333333329); // Average pace is distance / duration (5.0 / 0.5)
    }

    [Fact]
    public void AveragePace_ShouldBeNullWhenDurationIsNull()
    {
        // Arrange
        var distance = 5.0;
        var duration = (TimeSpan?)null;

        // Act
        var averagePace = CalculateAveragePace(distance, duration);

        // Assert
        averagePace.Should().BeNull();
    }

    [Fact]
    public void Duration_ShouldBeNullWhenDateTimeEndedIsNull()
    {
        // Arrange
        var dateTimeStarted = new DateTime(2024, 1, 1, 10, 0, 0);

        // Act
        var activity = new Activity
        {
            DateTimeStarted = dateTimeStarted,
            Location = _location,
            UserProfileId = _userProfile.Id,
        };

        // Assert
        activity.Duration.Should().BeNull();
    }

    [Fact]
    public void Duration_ShouldBeCalculatedCorrectly()
    {
        // Arrange
        var activity = new Activity
        {
            DateTimeStarted = new DateTime(2024, 7, 1, 8, 0, 0),
            DateTimeEnded = new DateTime(2024, 7, 1, 9, 30, 0),
            Distance = 10,// km
            Location = "People's Park", 
            UserProfileId = _userProfile.Id,
        };

        // Act
        var duration = activity.Duration;

        // Assert
        Assert.Equal(new TimeSpan(1, 30, 0), duration); // 1 hour 30 minutes
    }

    [Fact]
    public void AveragePace_ShouldBeCalculatedCorrectly()
    {
        // Arrange
        var activity = new Activity
        {
            DateTimeStarted = new DateTime(2024, 7, 1, 8, 0, 0),
            DateTimeEnded = new DateTime(2024, 7, 1, 9, 30, 0),
            Distance = 10, // km
            Location = "People's Park",
            UserProfileId = _userProfile.Id,
        };

        // Act
        var averagePace = activity.AveragePace;

        // Assert
        Assert.Equal(9.0, averagePace ?? 0, 1); // 90 minutes / 10 km = 9.0 minutes per kilometer
    }

    [Fact]
    public void Duration_ShouldUpdate_WhenDateTimeEndedChanges()
    {
        // Arrange
        var activity = new Activity
        {
            DateTimeStarted = new DateTime(2024, 7, 1, 8, 0, 0),
            DateTimeEnded = new DateTime(2024, 7, 1, 9, 0, 0),
            Distance = 10, // km
            Location = "People's Park",
            UserProfileId = _userProfile.Id,
        };

        // Act
        activity.DateTimeEnded = new DateTime(2024, 7, 1, 10, 0, 0);
        var duration = activity.Duration;

        // Assert
        Assert.Equal(new TimeSpan(2, 0, 0), duration); // 2 hours
    }

    [Fact]
    public void AveragePace_ShouldUpdate_WhenDateTimeEndedChanges()
    {
        // Arrange
        var activity = new Activity
        {
            DateTimeStarted = new DateTime(2024, 7, 1, 8, 0, 0),
            DateTimeEnded = new DateTime(2024, 7, 1, 9, 0, 0),
            Distance = 10, // km
            Location = "People's Park",
            UserProfileId = _userProfile.Id,
        };

        // Act
        activity.DateTimeEnded = new DateTime(2024, 7, 1, 10, 0, 0);
        var averagePace = activity.AveragePace;

        // Assert
        Assert.Equal(12.0, averagePace ?? 0, 1); // 120 minutes / 10 km = 12.0 minutes per kilometer
    }

    [Fact]
    public void AveragePace_ShouldBeZero_WhenDistanceIsZero()
    {
        // Arrange
        var activity = new Activity
        {
            DateTimeStarted = new DateTime(2024, 7, 1, 8, 0, 0),
            DateTimeEnded = new DateTime(2024, 7, 1, 9, 30, 0),
            Distance = 0, // km
            Location = "People's Park",
            UserProfileId = _userProfile.Id,
        };

        // Act
        var averagePace = activity.AveragePace;

        // Assert
        Assert.Equal(0, averagePace); // to note: div by zero should be avoided
    }

    /// <summary>
    /// Pace in minutes per kilometer
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private double? CalculateAveragePace(double? distance, TimeSpan? duration)
    {
        if (distance == null || duration == null || duration == TimeSpan.Zero)
            return null;

        return distance / duration.Value.TotalMinutes;
    }
}