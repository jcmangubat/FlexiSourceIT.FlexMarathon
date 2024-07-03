using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;
using static FlexiSourceIT.FlexMarathon.Domain.Constants.Rules;

namespace FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;

/// <summary>
/// For the given calculated fields, implementing calculation directly in this entity class 
/// using properties is typically sufficient, as it keeps the calculations encapsulated within 
/// the entity and ensures they are always up-to-date or real-time. 
/// </summary>
public class UserProfile : GuidKeyedEntity
{
    private double _weight;
    private double _height;
    private DateTime _birthDate;

    private int? _age;
    private double? _bmi;

    public required string Name { get; set; }
    public GendersEnum Gender { get; set; }

    /// <summary>
    /// Weight in kg
    /// </summary>
    public required double Weight
    {
        get => _weight;
        set
        {
            _weight = value;
            _bmi = CalculateBMI();
        }
    }

    /// <summary>
    /// Height in cm
    /// </summary>
    public required double Height
    {
        get => _height;
        set
        {
            _height = value;
            _bmi = CalculateBMI();
        }
    }

    public required DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            _birthDate = value;
            _age = CalculateAge();
        }
    }

    public int? Age { get => _age; set { _age = value; } }
    public double? BMI { get => _bmi; set { _bmi = value; } }

    public virtual List<Activity> Activities { get; set; } = [];

    internal int CalculateAge()
    {
        var today = DateTime.Today;
        var age = today.Year - BirthDate.Year;
        if (BirthDate.Date > today.AddYears(-age)) age--;
        return age;
    }

    internal double CalculateBMI() =>
        (_height > 0) ? _weight / Math.Pow(_height / 100, 2) : 0;
}
