using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;

public interface IActivityService
{
    Task<ActivityModel?> AddActivityAsync(ActivityModel activity);
    Task UpdateActivityAsync(ActivityModel activity);

    Task<ActivityModel?> GetActivityAsync(Guid activityId);
    Task<ActivityModel?> GetActivityAsync(Expression<Func<ActivityModel, bool>> modelFilter);
    Task<IEnumerable<ActivityModel>?> GetActivitiesAsync(Guid userProfileId);

    Task<ActivityModel?> GetActivityAsync(ActivityModel filter);
    Task<List<ActivityModel>?> GetActivitiesAsync(ActivityModel filter);

    Task<IEnumerable<ActivityModel>?> GetActivitiesByFilterAsync(Expression<Func<ActivityModel, bool>> filterExpression);
}