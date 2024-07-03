using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FluentAssertions;
using static FlexiSourceIT.FlexMarathon.Domain.Constants.Rules;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Domain.Entities;

public class UserProfileTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var name = "John Doe";
        var gender = GendersEnum.Male;
        var weight = 75.0;
        var height = 1.8;
        var birthDate = new DateTime(1990, 1, 1);

        // Act
        var userProfile = new UserProfile
        {
            Name = name,
            Gender = gender,
            Weight = weight,
            Height = height,
            BirthDate = birthDate,
            Age = CalculateAge(birthDate),
            BMI = CalculateBMI(weight, height)
        };

        // Assert
        userProfile.Name.Should().Be(name);
        userProfile.Gender.Should().Be(gender);
        userProfile.Weight.Should().Be(weight);
        userProfile.Height.Should().Be(height);
        userProfile.BirthDate.Should().Be(birthDate);
        userProfile.Age.Should().Be(CalculateAge(birthDate));
        userProfile.BMI.Should().BeApproximately(CalculateBMI(weight, height), 0.01);
    }

    [Fact]
    public void BMI_ShouldCalculateCorrectly()
    {
        // Arrange
        var weight = 75.0;
        var height = 1.8;

        // Act
        var bmi = CalculateBMI(weight, height);

        // Assert
        bmi.Should().BeApproximately(23.15, 0.01); // 75 / (1.8 * 1.8) ≈ 23.15
    }

    [Fact]
    public void Age_ShouldCalculateCorrectly()
    {
        // Arrange
        var birthDate = new DateTime(1990, 1, 1);

        // Act
        var age = CalculateAge(birthDate);

        // Assert
        age.Should().Be(34); // Assuming the current date is 2024-01-01
    }

    [Fact]
    public void Weight_ShouldCalculateBMI_WhenWeightIsChanged()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Height = 170, // cm
            BirthDate = new DateTime(1990, 1, 1), // BirthDate
            Weight = 0,
            Name = "Juan dela Cruz",
        };

        // Act
        userProfile.Weight = 70; // kg

        // Assert
        Assert.Equal(24.22, userProfile.BMI ?? 0, 2); // BMI = 70 / (1.7^2) ≈ 24.22
    }

    [Fact]
    public void Height_ShouldCalculateBMI_WhenHeightIsChanged()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Weight = 70, // kg
            BirthDate = new DateTime(1990, 1, 1), // BirthDate
            Name = "Juan dela Cruz",
            Height = 0
        };

        // Act
        userProfile.Height = 180; // cm

        // Assert
        Assert.Equal(21.60, userProfile.BMI ?? 0, 2); // BMI = 70 / (1.8^2) ≈ 21.60
    }

    [Fact]
    public void BirthDate_ShouldCalculateAge_WhenBirthDateIsChanged()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Weight = 70, // kg
            Height = 170, // cm
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(2008, 7, 23)
        };

        // Act
        userProfile.BirthDate = new DateTime(2000, 1, 1); // New BirthDate

        // Assert
        Assert.Equal(24, userProfile.Age); // Assuming the current date is 2024-07-03
    }

    [Fact]
    public void Weight_ShouldUpdateBMI_WhenWeightIsSet()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Height = 160, // cm
            BirthDate = new DateTime(1990, 1, 1), // BirthDate
            Name = "Juan dela Cruz",
            Weight = 0
        };

        // Act
        userProfile.Weight = 80; // kg

        // Assert
        Assert.Equal(31.25, userProfile.BMI ?? 0, 2); // BMI = 80 / (1.6^2) ≈ 31.25
    }

    [Fact]
    public void Height_ShouldUpdateBMI_WhenHeightIsSet()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Weight = 80, // kg
            BirthDate = new DateTime(1990, 1, 1), // BirthDate
            Name = "Juan dela Cruz",
            Height = 0
        };

        // Act
        userProfile.Height = 175; // cm

        // Assert
        Assert.Equal(26.12, userProfile.BMI ?? 0, 2); // BMI = 80 / (1.75^2) ≈ 26.12
    }

    [Fact]
    public void BirthDate_ShouldUpdateAge_WhenBirthDateIsSet()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Weight = 80, // kg
            Height = 175, // cm
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(2008, 7, 23)
        };

        // Act
        userProfile.BirthDate = new DateTime(1985, 5, 15); // New BirthDate

        // Assert
        Assert.Equal(39, userProfile.Age); // Assuming the current date is 2024-07-03
    }

    [Fact]
    public void SettingWeightAndHeight_ShouldUpdateBMI()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Height = 175, // cm
            Weight = 75, // kg
            BirthDate = new DateTime(1985, 5, 15), // BirthDate
            Name = "Juan dela Cruz",
        };

        // Act
        userProfile.Weight = 80; // kg
        userProfile.Height = 180; // cm

        // Assert
        Assert.Equal(24.69, userProfile.BMI ?? 0, 2); // BMI = 80 / (1.8^2) ≈ 24.69
    }

    [Fact]
    public void SettingBirthDate_ShouldUpdateAge()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Height = 175, // cm
            Weight = 75, // kg
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(2008, 7, 23)
        };

        // Act
        userProfile.BirthDate = new DateTime(1990, 1, 1); // New BirthDate

        // Assert
        Assert.Equal(34, userProfile.Age); // Assuming the current date is 2024-07-03
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }

    private static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height);
    }
}
