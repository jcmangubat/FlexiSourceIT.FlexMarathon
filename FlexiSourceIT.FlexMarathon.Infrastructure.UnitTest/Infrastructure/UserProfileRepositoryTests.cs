using FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Repositories;
using FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures;
using FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using static FlexiSourceIT.FlexMarathon.Domain.Constants.Rules;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure;

public class UserProfileRepositoryTests : IClassFixture<UserProfileRepositoryTestsFixture>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly RepositoryTestsFixture<UserProfileRepository, UserProfile> _fixture;

    public UserProfileRepositoryTests(UserProfileRepositoryTestsFixture fixture)
    {
        _fixture = fixture.Fixture;
        _userProfileRepository = _fixture.Repository;
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            Gender = GendersEnum.Male,
            Weight = 70.0,
            Height = 175.0,
            BirthDate = new DateTime(1989, 10, 1),
            Age = 34,
            BMI = 22.8
        };

        // Act
        await _userProfileRepository.AddAsync(userProfile);
        await _userProfileRepository.CommitAsync();

        // Assert
        var addedUserProfile = await _fixture.DbContext.UserProfiles.FirstOrDefaultAsync(up => up.Name == "Juan dela Cruz");
        addedUserProfile.Should().NotBeNull();
        addedUserProfile?.Name.Should().Be("Juan dela Cruz");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEntity()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Id = Guid.NewGuid(),
            Name = "Benito Brown",
            Gender = GendersEnum.Male,
            Weight = 70.0,
            Height = 175.0,
            BirthDate = new DateTime(1995, 1, 1),
            Age = 34,
            BMI = 22.8
        };

        await _userProfileRepository.AddAsync(userProfile);
        await _userProfileRepository.CommitAsync();

        // Act
        await _userProfileRepository.DeleteAsync(userProfile);
        await _userProfileRepository.CommitAsync();

        // Assert
        var deletedUserProfile = await _fixture.DbContext.UserProfiles.FirstOrDefaultAsync(up => up.Name == "Benito Brown");
        deletedUserProfile.Should().BeNull();
    }

    [Fact]
    public async Task GetSingleAsync_ShouldReturnUserProfile()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            Gender = GendersEnum.Male,
            Weight = 70.0,
            Height = 175.0,
            BirthDate = new DateTime(1990, 1, 1),
            Age = 34,
            BMI = 22.8
        };

        await _userProfileRepository.AddAsync(userProfile);
        await _userProfileRepository.CommitAsync();

        // Act
        var fetchedUserProfile = await _userProfileRepository.GetSingleAsync(up => up.Name == "Juan dela Cruz");

        // Assert
        fetchedUserProfile.Should().NotBeNull();
        fetchedUserProfile?.Name.Should().Be("Juan dela Cruz");
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnUserProfiles()
    {
        // Arrange
        var userProfile1 = new UserProfile
        {
            Id = Guid.NewGuid(),
            Name = "Henry Cunanan",
            Gender = GendersEnum.Male,
            Weight = 70.0,
            Height = 175.0,
            BirthDate = new DateTime(1990, 1, 1),
            Age = 34,
            BMI = 22.8
        };

        var userProfile2 = new UserProfile
        {
            Id = Guid.NewGuid(),
            Name = "Jane Fonda",
            Gender = GendersEnum.Female,
            Weight = 60.0,
            Height = 165.0,
            BirthDate = new DateTime(1992, 2, 2),
            Age = 32,
            BMI = 22.0
        };

        await _userProfileRepository.AddAsync(userProfile1, userProfile2);
        await _userProfileRepository.CommitAsync();

        // Act
        var userProfiles = await _userProfileRepository.GetListAsync(up => up.Age > 30);

        // Assert
        //userProfiles.Should().HaveCount(2);
        userProfiles.Should().Contain(up => up.Name == "Henry Cunanan");
        userProfiles.Should().Contain(up => up.Name == "Jane Fonda");
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyEntity()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            Gender = GendersEnum.Male,
            Weight = 70.0,
            Height = 175.0,
            BirthDate = new DateTime(1990, 1, 1),
            Age = 34,
            BMI = 22.8
        };

        await _userProfileRepository.AddAsync(userProfile);
        await _userProfileRepository.CommitAsync();

        // Act
        userProfile.Name = "Johnathan Doe";
        await _userProfileRepository.UpdateAsync(userProfile);
        await _userProfileRepository.CommitAsync();

        // Assert
        var updatedUserProfile = await _fixture.DbContext.UserProfiles.FirstOrDefaultAsync(up => up.Name == "Johnathan Doe");
        updatedUserProfile.Should().NotBeNull();
        updatedUserProfile?.Name.Should().Be("Johnathan Doe");
    }
}