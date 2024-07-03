
using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;

namespace FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;

/// <summary>
/// For the given calculated fields, implementing calculation directly in this entity class 
/// using properties is typically sufficient, as it keeps the calculations encapsulated within 
/// the entity and ensures they are always up-to-date or real-time. 
/// </summary>
public class Activity : GuidKeyedEntity
{
    private DateTime _dateTimeStarted;
    private DateTime? _dateTimeEnded;
    private TimeSpan? _duration = null;
    private double? _distance;
    private double? _averagePace;

    public required string Location { get; set; }
    public required DateTime DateTimeStarted
    {
        get => _dateTimeStarted;
        set
        {
            _dateTimeStarted = value;
            _duration = CalculateDuration();
        }
    }
    public DateTime? DateTimeEnded
    {
        get => _dateTimeEnded;
        set
        {
            _dateTimeEnded = value;
            _duration = CalculateDuration();
            _averagePace = CalculateAveragePace();
        }
    }

    public double? Distance
    {
        get => _distance;
        set
        {
            _distance = value;
            _averagePace = CalculateAveragePace();
        }
    }

    // Calculated field for Duration
    public TimeSpan? Duration
    {
        get => _duration;
        set => _duration = value;
    }

    // Calculated field for Average Pace (hours per kilometer)
    public double? AveragePace
    {
        get => _averagePace;
        set => _averagePace = value;
    }

    public required Guid UserProfileId { get; set; }
    public virtual UserProfile? UserProfile { get; set; }

    internal TimeSpan? CalculateDuration() =>
        _dateTimeEnded.HasValue ? _dateTimeEnded.Value - _dateTimeStarted : null;

    internal double? CalculateAveragePace() =>
        _distance > 0 && _duration != null && _duration.Value != TimeSpan.Zero
                                                ? _duration.Value.TotalMinutes / _distance : 0;
}