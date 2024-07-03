using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Repositories;
using FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures.Base;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures;

public class UserProfileRepositoryTestsFixture
{
    public RepositoryTestsFixture<UserProfileRepository, UserProfile> Fixture { get; private set; }

    public UserProfileRepositoryTestsFixture()
    {
        Fixture = RepositoryFixtureFactory.Create<UserProfileRepository, UserProfile>(dbContext => new UserProfileRepository(dbContext));
    }
}