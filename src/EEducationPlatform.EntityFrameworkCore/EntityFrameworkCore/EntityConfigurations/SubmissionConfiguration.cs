using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Submission));

        #region Properies configuration
    
        // Add properties configurations

        #endregion

        #region Keys configuration

        builder.HasOne<Exam>()
            .WithMany(exam => exam.Submissions)
            .HasForeignKey(submission => submission.ExamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(submission => submission.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}