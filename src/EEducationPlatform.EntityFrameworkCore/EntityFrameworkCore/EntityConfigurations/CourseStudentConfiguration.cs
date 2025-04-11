using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class CourseStudentConfiguration : IEntityTypeConfiguration<CourseStudent>
{
    public void Configure(EntityTypeBuilder<CourseStudent> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(CourseStudent));

        #region Properies configuration

        // Configure properties

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Students)
            .HasForeignKey(courseStudent => courseStudent.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(courseStudent => courseStudent.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}