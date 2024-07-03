using AutoMapper;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using SMEAppHouse.Core.CodeKits.Helpers.Expressions;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.Application.Services;

public class UserProfileService(IMapper mapper,
                            ILogger<UserProfileService> logger,
                            IUserProfileRepository UserProfileRepository) : IUserProfileService
{
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<UserProfileService> _logger = logger;
    private readonly IUserProfileRepository _UserProfileRepository = UserProfileRepository;

    public async Task<UserProfileModel?> AddUserProfileAsync(UserProfileModel UserProfile)
    {
        try
        {
            var efUserProfile = _mapper.Map<UserProfile>(UserProfile);
            await _UserProfileRepository.AddAsync(efUserProfile);
            await _UserProfileRepository.CommitAsync();

            UserProfile.Id = efUserProfile.Id;
            return UserProfile;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task UpdateUserProfileAsync(UserProfileModel UserProfile)
    {
        try
        {
            var efUserProfile = _mapper.Map<UserProfile>(UserProfile);
            await _UserProfileRepository.UpdateAsync(efUserProfile);
            await _UserProfileRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<IEnumerable<UserProfileModel>?> GetUserProfilesAsync(Expression<Func<UserProfileModel, bool>> modelFilter)
    {
        try
        {
            Expression<Func<UserProfile, bool>> efModelFilter = ExpressionConverter.Convert<UserProfile, UserProfileModel>(modelFilter);
            var userProfiles = await _UserProfileRepository.GetListAsync(filter: efModelFilter) ??
                            throw new Exception($"{nameof(UserProfile)} entry not found.");

            return _mapper.Map<List<UserProfileModel>>(userProfiles);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<UserProfileModel?> GetUserProfileAsync(Guid UserProfileId)
    {
        try
        {
            var UserProfile = await _UserProfileRepository.GetSingleAsync(p => p.Id == UserProfileId);
            return _mapper.Map<UserProfileModel>(UserProfile);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }


    public async Task<UserProfileModel?> GetUserProfileAsync(Expression<Func<UserProfileModel, bool>> modelFilter)
    {
        try
        {
            Expression<Func<UserProfile, bool>> efModelFilter = ExpressionConverter.Convert<UserProfile, UserProfileModel>(modelFilter);
            var userProfile = await _UserProfileRepository.GetSingleAsync(filterPredicate: efModelFilter) ??
                            throw new Exception($"{nameof(UserProfile)} entry not found.");

            return _mapper.Map<UserProfileModel>(userProfile);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }
}
