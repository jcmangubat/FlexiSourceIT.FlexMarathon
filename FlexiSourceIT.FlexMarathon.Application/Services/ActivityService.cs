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

public class ActivityService(IMapper mapper,
                            ILogger<ActivityService> logger,
                            IActivityRepository activityRepository) : IActivityService
{
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ActivityService> _logger = logger;
    private readonly IActivityRepository _activityRepository = activityRepository;

    public async Task<ActivityModel?> AddActivityAsync(ActivityModel activity)
    {
        try
        {
            var efActivity = _mapper.Map<Activity>(activity);
            await _activityRepository.AddAsync(efActivity);
            await _activityRepository.CommitAsync();

            activity.Id = efActivity.Id;
            return activity;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task UpdateActivityAsync(ActivityModel activity)
    {
        try
        {
            var efActivity = _mapper.Map<Activity>(activity);
            await _activityRepository.UpdateAsync(efActivity);
            await _activityRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<IEnumerable<ActivityModel>?> GetActivitiesAsync(Guid userProfileId)
    {
        try
        {
            var activities = await _activityRepository.GetListAsync(filter: p => p.UserProfileId == userProfileId);
            return _mapper.Map<List<ActivityModel>>(activities);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<ActivityModel?> GetActivityAsync(Guid activityId)
    {
        try
        {
            var activity = await _activityRepository.GetSingleAsync(p => p.Id == activityId);
            return _mapper.Map<ActivityModel>(activity);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }


    public async Task<ActivityModel?> GetActivityAsync(Expression<Func<ActivityModel, bool>> modelFilter)
    {
        try
        {
            Expression<Func<Activity, bool>> efModelFilter = ExpressionConverter.Convert<Activity, ActivityModel>(modelFilter);
            var activity = await _activityRepository.GetSingleAsync(filterPredicate: efModelFilter) ??
                            throw new Exception($"{nameof(Activity)} entry not found.");

            return _mapper.Map<ActivityModel>(activity);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<ActivityModel?> GetActivityAsync(ActivityModel filter)
    {
        try
        {
            var activity = await _activityRepository.GetSingleAsync(filter) ??
                            throw new Exception($"{nameof(Activity)} entry not found.");

            return _mapper.Map<ActivityModel>(activity);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<List<ActivityModel>?> GetActivitiesAsync(ActivityModel filter)
    {
        try
        {
            var activities = await _activityRepository.GetListAsync(filter) ??
                            throw new Exception($"{nameof(Activity)} entry not found.");

            return _mapper.Map<List<ActivityModel>>(activities);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<IEnumerable<ActivityModel>?> GetActivitiesByFilterAsync(Expression<Func<ActivityModel, bool>> filterExpression)
    {
        try
        {
            Expression<Func<Activity, bool>> efFilterExpression = ExpressionConverter.Convert<Activity, ActivityModel>(filterExpression);

            var activities = await _activityRepository.GetListAsync(efFilterExpression);
            return _mapper.Map<List<ActivityModel>>(activities);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }
}
