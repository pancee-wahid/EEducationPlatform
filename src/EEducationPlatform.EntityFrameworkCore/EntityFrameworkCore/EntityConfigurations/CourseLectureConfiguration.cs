using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class CourseLectureConfiguration : IEntityTypeConfiguration<CourseLecture>
{
    public void Configure(EntityTypeBuilder<CourseLecture> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(CourseLecture));

        #region Properies configuration

        builder.Property(x => x.Name).HasMaxLength(StringLength.Name);
        builder.Property(x => x.Description).HasMaxLength(StringLength.Description);
        builder.Property(x => x.YoutubeLink).HasMaxLength(StringLength.Link);

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Lectures)
            .HasForeignKey(courseLecture => courseLecture.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}