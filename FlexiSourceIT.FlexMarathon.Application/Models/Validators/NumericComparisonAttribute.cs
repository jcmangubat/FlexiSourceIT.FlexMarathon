using System.ComponentModel.DataAnnotations;

namespace SMEAppHouse.Ladon.Application.Models.Validators;

public class NumericComparisonAttribute(string comparisonProperty) : ValidationAttribute
{
    private readonly string _comparisonProperty = comparisonProperty;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        var highValueProperty = (double)value;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
            return new ValidationResult($"Unknown property: {_comparisonProperty}");

        var lowValuePropertyObj = property.GetValue(validationContext.ObjectInstance);
        if (lowValuePropertyObj == null || lowValuePropertyObj is not double)
        {
            // Handle the case where lowValuePropertyObj is null or not of type double
            return new ValidationResult($"Invalid low value property value for property: {_comparisonProperty}");
        }

        var lowValueProperty = (double)lowValuePropertyObj;
        if (highValueProperty > 0 && highValueProperty < lowValueProperty)
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than or equal to {_comparisonProperty}");

        return ValidationResult.Success;
    }
}
