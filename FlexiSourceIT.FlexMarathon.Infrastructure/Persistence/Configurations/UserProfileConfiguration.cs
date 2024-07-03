using FlexiSourceIT.FlexMarathon.Domain.Constants;
using FlexiSourceIT.FlexMarathon.Domain.Entities.EFModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using SMEAppHouse.Core.Patterns.EF.ModelCfgAbstractions;

namespace FlexiSourceIT.FlexMarathon.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration(string schema = "dbo")
    : EntityConfiguration<UserProfile, Guid>(prefixEntityNameToId: true,
        prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{

    public override void OnModelCreating(EntityTypeBuilder<UserProfile> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(x => x.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(x => x.Gender, true);
        entityBuilder.DefineDbField(x => x.Weight, true);
        entityBuilder.DefineDbField(x => x.Height, true);
        entityBuilder.DefineDbField(x => x.BirthDate, true);
        entityBuilder.DefineDbField(x => x.Age, false);
        entityBuilder.DefineDbField(x => x.BMI, false);
    }
}
