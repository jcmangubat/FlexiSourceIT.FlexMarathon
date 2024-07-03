using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.EF.ModelComposites.Interfaces;
using SMEAppHouse.Core.Patterns.Repo.Repository.Abstractions;

namespace FlexiSourceIT.FlexMarathon.UnitTest.Infrastructure.Fixtures.Base;

public class RepositoryTestsFixture<TRepository, TEntity> : IDisposable
    where TEntity : class, IEntityKeyed<Guid>
    where TRepository : class, IRepositoryForKeyedEntity<TEntity, Guid>
{
    public DbContextOptions<ApplicationDbContext> DbContextOptions { get; private set; }
    public ApplicationDbContext DbContext { get; private set; }
    public TRepository Repository { get; private set; }

    public RepositoryTestsFixture(Func<ApplicationDbContext, TRepository> repositoryFactory)
    {
        DbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())  // Use a unique name for the in-memory database
            .Options;

        DbContext = new ApplicationDbContext(DbContextOptions);
        Repository = repositoryFactory(DbContext);
    }

    public void Dispose()
    {
        DbContext?.Dispose();
    }
}
