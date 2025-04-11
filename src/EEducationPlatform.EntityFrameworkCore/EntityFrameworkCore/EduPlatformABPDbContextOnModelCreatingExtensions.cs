using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EEducationPlatform.EntityFrameworkCore;

public static class EEducationPlatformDbContextOnModelCreatingExtensions
{
    public static void ConfigureEEducationPlatformEntities(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EEducationPlatformConsts.DbTablePrefix + "YourEntities", EEducationPlatformConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}