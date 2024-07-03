using FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using SMEAppHouse.Core.Patterns.Repo.Repository.Abstractions;

namespace FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Repositories;

public class UserProfileRepository(ApplicationDbContext dbContext)
    : RepositoryForKeyedEntity<UserProfile, Guid>(dbContext), IUserProfileRepository
{

}
