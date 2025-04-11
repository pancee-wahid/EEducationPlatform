using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.Aggregates.LookupTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class LookupTypeConfiguration : IEntityTypeConfiguration<LookupType>
{
    public void Configure(EntityTypeBuilder<LookupType> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(LookupType));

        #region Properies configuration

        builder.Property(x => x.Name).HasMaxLength(StringLength.Name);
        builder.Property(x => x.Code).HasMaxLength(StringLength.Code);
        builder.Property(x => x.Description).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        // Configure keys

        #endregion
    }
}