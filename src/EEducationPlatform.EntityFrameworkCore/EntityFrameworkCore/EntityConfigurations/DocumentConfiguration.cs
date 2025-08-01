using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Document));

        #region Properies configuration

        builder.Property(x => x.Name).HasMaxLength(StringLength.Name);
        builder.Property(x => x.Path).HasMaxLength(StringLength.Path);

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Documents)
            .HasForeignKey(courseDocument => courseDocument.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Lecture>()
            .WithMany()
            .HasForeignKey(courseDocument => courseDocument.LectureId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}