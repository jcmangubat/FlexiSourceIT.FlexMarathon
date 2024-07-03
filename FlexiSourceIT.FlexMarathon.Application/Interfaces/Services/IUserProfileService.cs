using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;

public interface IUserProfileService
{
    Task<UserProfileModel?> AddUserProfileAsync(UserProfileModel userProfile);
    Task UpdateUserProfileAsync(UserProfileModel userProfile);

    Task<UserProfileModel?> GetUserProfileAsync(Guid userProfileId);
    Task<UserProfileModel?> GetUserProfileAsync(Expression<Func<UserProfileModel, bool>> modelFilter);
    Task<IEnumerable<UserProfileModel>?> GetUserProfilesAsync(Expression<Func<UserProfileModel, bool>> modelFilter);
}