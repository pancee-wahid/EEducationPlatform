using EEducationPlatform.Aggregates.LookupTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class LookupValueConfiguration : IEntityTypeConfiguration<LookupValue>
{
    public void Configure(EntityTypeBuilder<LookupValue> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(LookupValue));

        #region Properies configuration

        builder.Property(x => x.Name).HasMaxLength(StringLength.Name);
        builder.Property(x => x.Code).HasMaxLength(StringLength.Code);
        builder.Property(x => x.Description).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        builder.HasOne<LookupType>()
            .WithMany(lookupType => lookupType.LookupValues)
            .HasForeignKey(lookupValue => lookupValue.LookupTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}