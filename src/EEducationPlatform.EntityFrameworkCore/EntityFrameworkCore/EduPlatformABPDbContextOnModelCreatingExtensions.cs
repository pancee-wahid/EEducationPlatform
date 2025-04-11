using EEducationPlatform.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EEducationPlatform.EntityFrameworkCore;

public static class EEducationPlatformDbContextOnModelCreatingExtensions
{
    public static void ConfigureEEducationPlatformEntities(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure your own tables/entities inside here */
        
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new CourseConfiguration());
        builder.ApplyConfiguration(new CourseInstructorConfiguration());
        builder.ApplyConfiguration(new CourseAdminConfiguration());
        builder.ApplyConfiguration(new CourseStudentConfiguration());
        builder.ApplyConfiguration(new CourseCategoryConfiguration());
        builder.ApplyConfiguration(new CourseLectureConfiguration());
        builder.ApplyConfiguration(new CourseDocumentConfiguration());
        builder.ApplyConfiguration(new LookupTypeConfiguration());
        builder.ApplyConfiguration(new LookupValueConfiguration());
        
        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EEducationPlatformConsts.DbTablePrefix + "YourEntities", EEducationPlatformConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}