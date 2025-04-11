using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class CourseDocumentConfiguration : IEntityTypeConfiguration<CourseDocument>
{
    public void Configure(EntityTypeBuilder<CourseDocument> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(CourseDocument));

        #region Properies configuration

        builder.Property(x => x.Name).HasMaxLength(StringLength.Name);
        builder.Property(x => x.Path).HasMaxLength(StringLength.Path);

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Documents)
            .HasForeignKey(courseDocument => courseDocument.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<CourseLecture>()
            .WithMany()
            .HasForeignKey(courseDocument => courseDocument.LectureId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}