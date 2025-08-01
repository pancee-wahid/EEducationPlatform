using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
{
    public void Configure(EntityTypeBuilder<StudentAnswer> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(StudentAnswer));

        #region Properies configuration

        builder.Property(x => x.Answer).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        builder.HasOne<Submission>()
            .WithMany(submission => submission.Answers)
            .HasForeignKey(studentAnswer => studentAnswer.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Question>()
            .WithMany()
            .HasForeignKey(studentAnswer => studentAnswer.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}