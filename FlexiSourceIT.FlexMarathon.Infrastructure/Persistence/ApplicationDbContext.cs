using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.EF.SettingsModel;

namespace FlexiSourceIT.FlexMarathon.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                    DbMigrationInformation? dbMigrationInformation = null) 
    : DbContext(options)
{
    private readonly DbMigrationInformation _dbMigrationInformation = dbMigrationInformation;

    public virtual DbSet<Activity> Activities { get; set; }
    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var dbSchema = "dbo";
        builder.HasDefaultSchema(dbSchema);

        // Additional configurations
        if (_dbMigrationInformation != null
            && !string.IsNullOrEmpty(_dbMigrationInformation.DbSchema))
        {
            dbSchema = _dbMigrationInformation.DbSchema;
            builder.HasDefaultSchema(_dbMigrationInformation.DbSchema);
        }

        builder.ApplyConfiguration(new ActivityConfiguration(dbSchema));
        builder.ApplyConfiguration(new UserProfileConfiguration(dbSchema));
    }
}
