using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence;
using SMEAppHouse.Core.Patterns.EF.ModelComposites.Interfaces;
using SMEAppHouse.Core.Patterns.Repo.Repository.Abstractions;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures.Base;

public static class RepositoryFixtureFactory
{
    public static RepositoryTestsFixture<TRepository, TEntity> Create<TRepository, TEntity>(
        Func<ApplicationDbContext, TRepository> repositoryFactory)
        where TEntity : class, IEntityKeyed<Guid>
        where TRepository : class, IRepositoryForKeyedEntity<TEntity, Guid>
    {
        return new RepositoryTestsFixture<TRepository, TEntity>(repositoryFactory);
    }
}