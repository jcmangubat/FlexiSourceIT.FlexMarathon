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

public class UserProfileServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<UserProfileService>> _loggerMock;
    private readonly Mock<IUserProfileRepository> _userProfileRepositoryMock;
    private readonly UserProfileService _userProfileService;

    public UserProfileServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<UserProfileService>>();
        _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
        _userProfileService = new UserProfileService(_mapperMock.Object, _loggerMock.Object, _userProfileRepositoryMock.Object);
    }

    [Fact]
    public async Task AddUserProfileAsync_ShouldAddUserProfileAndReturnModel()
    {
        // Arrange
        var userProfileModel = new UserProfileModel
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };
        var userProfile = new UserProfile
        {
            Id = userProfileModel.Id,
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };

        _mapperMock.Setup(m => m.Map<UserProfile>(userProfileModel)).Returns(userProfile);

        // Act
        var result = await _userProfileService.AddUserProfileAsync(userProfileModel);

        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(userProfileModel.Id);

        _userProfileRepositoryMock.Verify(r => r.AddAsync(userProfile), Times.Once);
        _userProfileRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateUserProfileAsync_ShouldUpdateUserProfile()
    {
        // Arrange
        var userProfileModel = new UserProfileModel
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };
        var userProfile = new UserProfile
        {
            Id = userProfileModel.Id,
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };

        _mapperMock.Setup(m => m.Map<UserProfile>(userProfileModel)).Returns(userProfile);

        // Act
        await _userProfileService.UpdateUserProfileAsync(userProfileModel);

        // Assert
        _userProfileRepositoryMock.Verify(r => r.UpdateAsync(userProfile), Times.Once);
        _userProfileRepositoryMock.Verify(r => r.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task GetUserProfilesAsync_ShouldReturnUserProfiles()
    {
        // Arrange
        var userProfiles = new List<UserProfile>
        {
            new() { 
                Id = Guid.NewGuid(), 
                Name = "Juan dela Cruz",
                BirthDate = new DateTime(1980, 9, 2),
                Height = 122,
                Weight = 70.4
            },
            new() { 
                Id = Guid.NewGuid(), 
                Name = "Femy Mancera",
                BirthDate = new DateTime(1980, 9, 2),
                Height = 122,
                Weight = 70.4
            }
        };

        _userProfileRepositoryMock.Setup(r => r.GetListAsync(It.IsAny<Expression<Func<UserProfile, bool>>>()))
                                  .ReturnsAsync(userProfiles);

        var userProfileModels = new List<UserProfileModel>
        {
            new() { 
                Id = userProfiles[0].Id,
                Name = "Juan dela Cruz",
                BirthDate = new DateTime(1980, 9, 2),
                Height = 122,
                Weight = 70.4
            },
            new() { 
                Id = userProfiles[1].Id,
                Name = "Femy Mancera",
                BirthDate = new DateTime(1980, 9, 2),
                Height = 122,
                Weight = 70.4
            }
        };

        _mapperMock.Setup(m => m.Map<List<UserProfileModel>>(userProfiles)).Returns(userProfileModels);

        // Act
        var result = await _userProfileService.GetUserProfilesAsync(u => u.Name.Contains("Juan"));

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);

        _userProfileRepositoryMock.Verify(r => r.GetListAsync(It.IsAny<Expression<Func<UserProfile, bool>>>()), Times.Once);
    }

    [Fact]
    public async Task GetUserProfileAsync_ShouldReturnUserProfile()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var userProfile = new UserProfile
        {
            Id = userProfileId,
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };

        _userProfileRepositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<UserProfile, bool>>>()))
                                  .ReturnsAsync(userProfile);

        var userProfileModel = new UserProfileModel
        {
            Id = userProfileId,
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };

        _mapperMock.Setup(m => m.Map<UserProfileModel>(userProfile)).Returns(userProfileModel);

        // Act
        var result = await _userProfileService.GetUserProfileAsync(userProfileId);

        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(userProfileId);

        _userProfileRepositoryMock.Verify(r => r.GetSingleAsync(It.IsAny<Expression<Func<UserProfile, bool>>>()), Times.Once);
    }

    [Fact]
    public async Task GetUserProfileAsync_WithModelFilter_ShouldReturnUserProfile()
    {
        // Arrange
        var userProfile = new UserProfile
        {
            Id = Guid.NewGuid(),
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };

        _userProfileRepositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<UserProfile, bool>>>()))
                                  .ReturnsAsync(userProfile);

        var userProfileModel = new UserProfileModel
        {
            Id = userProfile.Id,
            Name = "Juan dela Cruz",
            BirthDate = new DateTime(1980, 9, 2),
            Height = 122,
            Weight = 70.4
        };

        _mapperMock.Setup(m => m.Map<UserProfileModel>(userProfile)).Returns(userProfileModel);

        // Act
        var result = await _userProfileService.GetUserProfileAsync(u => u.Name == "John Doe");

        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(userProfile.Id);

        _userProfileRepositoryMock.Verify(r => r.GetSingleAsync(It.IsAny<Expression<Func<UserProfile, bool>>>()), Times.Once);
    }
}