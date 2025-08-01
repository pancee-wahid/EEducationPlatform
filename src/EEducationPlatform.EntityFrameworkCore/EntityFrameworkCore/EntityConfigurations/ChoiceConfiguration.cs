using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Choice));

        #region Properies configuration

        builder.Property(x => x.Text).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        builder.HasOne<Question>()
            .WithMany(question => question.Choices)
            .HasForeignKey(choice => choice.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}