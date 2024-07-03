using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using SMEAppHouse.Core.Patterns.Repo.Repository.Abstractions;

namespace FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;

public interface IActivityRepository : IRepositoryForKeyedEntity<Activity, Guid>
{
    Task<Activity?> GetSingleAsync(ActivityModel filter);
    Task<List<Activity>?> GetListAsync(ActivityModel filter);
}
