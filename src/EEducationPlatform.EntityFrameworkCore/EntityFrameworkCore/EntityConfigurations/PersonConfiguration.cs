using EEducationPlatform.Aggregates.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Person));

        #region Properies configuration

        builder.Property(x => x.FullNameAr).HasMaxLength(StringLength.FullName);
        builder.Property(x => x.FullNameEn).HasMaxLength(StringLength.FullName);
        builder.Property(x => x.FirstNameAr).HasMaxLength(StringLength.Name);
        builder.Property(x => x.FirstNameEn).HasMaxLength(StringLength.Name);
        builder.Property(x => x.LastNameAr).HasMaxLength(StringLength.Name);
        builder.Property(x => x.LastNameEn).HasMaxLength(StringLength.Name);

        #endregion

        #region Keys configuration

        builder.HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(person => person.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}