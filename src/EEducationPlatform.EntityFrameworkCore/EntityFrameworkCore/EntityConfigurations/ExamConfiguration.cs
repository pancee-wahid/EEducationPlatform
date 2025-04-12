using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Exam));

        #region Properies configuration

        builder.Property(x => x.Name).HasMaxLength(StringLength.Name);
        builder.Property(x => x.Description).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Exams)
            .HasForeignKey(courseExam => courseExam.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Lecture>()
            .WithMany()
            .HasForeignKey(courseExam => courseExam.LectureId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}