using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.Aggregates.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Instructor));

        #region Properies configuration

        builder.Property(x => x.Experience).HasMaxLength(StringLength.Description);
        builder.Property(x => x.Bio).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Instructors)
            .HasForeignKey(courseInstructor => courseInstructor.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Person>()
            .WithMany()
            .HasForeignKey(person => person.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}