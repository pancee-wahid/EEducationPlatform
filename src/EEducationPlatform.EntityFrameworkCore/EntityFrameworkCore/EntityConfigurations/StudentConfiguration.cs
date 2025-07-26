using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.Aggregates.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Student));

        #region Properies configuration

        // Configure properties

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Students)
            .HasForeignKey(courseStudent => courseStudent.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Person>()
            .WithMany()
            .HasForeignKey(person => person.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}