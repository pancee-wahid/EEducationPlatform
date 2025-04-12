using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Question));

        #region Properies configuration

        builder.Property(x => x.Content).HasMaxLength(StringLength.Description);
        builder.Property(x => x.CorrectAnswer).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        builder.HasOne<Exam>()
            .WithMany(courseExam => courseExam.Questions)
            .HasForeignKey(courseExamQuestion => courseExamQuestion.ExamId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}