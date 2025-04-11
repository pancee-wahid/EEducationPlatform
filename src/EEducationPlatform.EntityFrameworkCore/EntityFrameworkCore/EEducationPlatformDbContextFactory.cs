using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EEducationPlatform.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class EEducationPlatformDbContextFactory : IDesignTimeDbContextFactory<EEducationPlatformDbContext>
{
    public EEducationPlatformDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        EEducationPlatformEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<EEducationPlatformDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);
        
        return new EEducationPlatformDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EEducationPlatform.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
