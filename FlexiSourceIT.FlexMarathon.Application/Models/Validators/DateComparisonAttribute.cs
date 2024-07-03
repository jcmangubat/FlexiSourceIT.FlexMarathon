using System.ComponentModel.DataAnnotations;

namespace SMEAppHouse.Ladon.Application.Models.Validators;

public class DateComparisonAttribute(string comparisonProperty) : ValidationAttribute
{
    private readonly string _comparisonProperty = comparisonProperty;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        var endDate = (DateTime)value;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
            return new ValidationResult($"Unknown property: {_comparisonProperty}");

        var startDateObj = property.GetValue(validationContext.ObjectInstance);
        if (startDateObj == null || startDateObj is not DateTime)
        {
            // Handle the case where startDateObj is null or not of type DateTime
            return new ValidationResult($"Invalid start date value for property: {_comparisonProperty}");
        }

        var startDate = (DateTime)startDateObj;
        if (endDate < startDate)
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than or equal to {_comparisonProperty}");

        return ValidationResult.Success;
    }
}

