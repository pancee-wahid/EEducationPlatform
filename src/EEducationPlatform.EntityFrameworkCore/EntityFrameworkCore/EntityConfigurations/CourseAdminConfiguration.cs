using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class CourseAdminConfiguration : IEntityTypeConfiguration<CourseAdmin>
{
    public void Configure(EntityTypeBuilder<CourseAdmin> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(CourseAdmin));

        #region Properies configuration

        builder.Property(x => x.Experience).HasMaxLength(StringLength.Description);
        builder.Property(x => x.Bio).HasMaxLength(StringLength.Description);

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Admins)
            .HasForeignKey(courseAdmin => courseAdmin.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(courseAdmin => courseAdmin.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}