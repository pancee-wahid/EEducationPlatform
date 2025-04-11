using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Aggregates.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class CourseCategoryConfiguration : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(CourseCategory));

        #region Properies configuration

        // Configure properties

        #endregion

        #region Keys configuration

        builder.HasOne<Course>()
            .WithMany(course => course.Categories)
            .HasForeignKey(courseCategory => courseCategory.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(courseCategory => courseCategory.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}