using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.Aggregates.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Admin));
        
        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Admins)
            .HasForeignKey(courseAdmin => courseAdmin.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Person>()
            .WithMany()
            .HasForeignKey(courseAdmin => courseAdmin.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}