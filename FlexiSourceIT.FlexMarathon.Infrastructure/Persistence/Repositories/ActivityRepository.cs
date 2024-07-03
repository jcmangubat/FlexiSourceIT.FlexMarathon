using FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo.Repository.GuidPKBasedVariation;

namespace FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Repositories;

public class ActivityRepository(ApplicationDbContext dbContext) :
    EntityRepositoryForKeyedEntity<Activity>(dbContext), IActivityRepository
{
    public async Task<List<Activity>?> GetListAsync(ActivityModel filter)
    {
        var query = ((ApplicationDbContext)DbContext).Activities.AsQueryable();
        var filteredQuery = filter.ApplyFilterTo(query);
        return await filteredQuery.ToListAsync();
    }

    public async Task<Activity?> GetSingleAsync(ActivityModel filter)
    {
        var query = ((ApplicationDbContext)DbContext).Activities.AsQueryable();
        var filteredQuery = filter.ApplyFilterTo(query);
        return await filteredQuery.FirstOrDefaultAsync();
    }
}

