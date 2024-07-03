using FlexiSourceIT.FlexMarathon.API.Helpers;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FluentAssertions;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.UnitTest;

public class FilterParserTests
{
    [Fact]
    public void Parse_ValidFilter_ReturnsExpression()
    {
        // Arrange
        var filter = "Name == 'Juan dela Cruz'";

        // Act
        var expression = FilterParser.Parse<UserProfileModel>(filter);

        // Assert
        expression.Should().NotBeNull();
    }

    [Fact]
    public void Parse_InvalidFilter_ThrowsArgumentException()
    {
        // Arrange
        var filter = "Name == Juan dela Cruz";  // Invalid filter

        // Act
        Func<Expression<Func<UserProfileModel, bool>>> act = () => FilterParser.Parse<UserProfileModel>(filter);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage($"Invalid filter expression: '{filter}'. Please ensure the syntax is correct.");
    }

    [Fact]
    public void Parse_EmptyFilter_ThrowsArgumentException()
    {
        // Arrange
        var filter = "";

        // Act
        Func<Expression<Func<UserProfileModel, bool>>> act = () => FilterParser.Parse<UserProfileModel>(filter);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("Filter cannot be null or empty.");
    }

    [Fact]
    public void Parse_ValidFilterWithSpecialCharacters_ReturnsExpression()
    {
        // Arrange
        var filter = "Name == 'O\\'Connor'";

        // Act
        var expression = FilterParser.Parse<UserProfileModel>(filter);

        // Assert
        expression.Should().NotBeNull();
    }

    [Fact]
    public void Parse_ValidFilterWithEscapedQuotes_ReturnsExpression()
    {
        // Arrange
        var filter = "Name == 'O''Connor'";

        // Act
        var expression = FilterParser.Parse<UserProfileModel>(filter);

        // Assert
        expression.Should().NotBeNull();
    }
}