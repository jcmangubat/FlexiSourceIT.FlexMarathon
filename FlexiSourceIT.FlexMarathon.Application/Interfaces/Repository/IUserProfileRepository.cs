using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using SMEAppHouse.Core.Patterns.Repo.Repository.Abstractions;

namespace FlexiSourceIT.FlexMarathon.Application.Interfaces.Repository;

public interface IUserProfileRepository : IRepositoryForKeyedEntity<UserProfile, Guid>
{
}
