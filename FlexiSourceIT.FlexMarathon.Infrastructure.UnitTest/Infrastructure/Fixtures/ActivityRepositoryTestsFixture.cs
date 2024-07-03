using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Repositories;
using FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures.Base;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures;

public class ActivityRepositoryTestsFixture
{
    public RepositoryTestsFixture<ActivityRepository, Activity> Fixture { get; private set; }

    public ActivityRepositoryTestsFixture()
    {
        Fixture = RepositoryFixtureFactory.Create<ActivityRepository, Activity>(dbContext => new ActivityRepository(dbContext));
    }
}
