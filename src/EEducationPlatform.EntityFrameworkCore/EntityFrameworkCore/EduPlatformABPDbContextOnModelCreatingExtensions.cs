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
        
        builder.ApplyConfiguration(new LookupTypeConfiguration());
        builder.ApplyConfiguration(new LookupValueConfiguration());
        
        builder.ApplyConfiguration(new CategoryConfiguration());
        
        builder.ApplyConfiguration(new CourseConfiguration());
        builder.ApplyConfiguration(new InstructorConfiguration());
        builder.ApplyConfiguration(new AdminConfiguration());
        builder.ApplyConfiguration(new StudentConfiguration());
        builder.ApplyConfiguration(new CourseCategoryConfiguration());
        builder.ApplyConfiguration(new LectureConfiguration());
        builder.ApplyConfiguration(new DocumentConfiguration());
        builder.ApplyConfiguration(new ExamConfiguration());
        builder.ApplyConfiguration(new QuestionConfiguration());
        builder.ApplyConfiguration(new ChoiceConfiguration());
        builder.ApplyConfiguration(new SubmissionConfiguration());
        builder.ApplyConfiguration(new StudentAnswerConfiguration());
        
        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EEducationPlatformConsts.DbTablePrefix + "YourEntities", EEducationPlatformConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}