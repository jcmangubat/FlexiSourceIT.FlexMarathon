using FlexiSourceIT.FlexMarathon.Domain.Constants;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using SMEAppHouse.Core.Patterns.EF.ModelCfgAbstractions;

namespace FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Configurations;

public class ActivityConfiguration(string schema = "dbo")
    : EntityConfiguration<Activity, Guid>(prefixEntityNameToId: true,
        prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Activity> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(x => x.Location, true, FieldLengths.General.Medium);
        entityBuilder.DefineDbField(x => x.DateTimeStarted, true);
        entityBuilder.DefineDbField(x => x.DateTimeEnded, false);
        entityBuilder.DefineDbField(x => x.Distance, false);
        entityBuilder.DefineDbField(x => x.Duration, false);
        entityBuilder.DefineDbField(x => x.AveragePace, false);;

        entityBuilder.HasOne(p => p.UserProfile)
                        .WithMany(p => p.Activities)
                        .HasForeignKey(p => p.UserProfileId)
                        .IsRequired();
    }
}

