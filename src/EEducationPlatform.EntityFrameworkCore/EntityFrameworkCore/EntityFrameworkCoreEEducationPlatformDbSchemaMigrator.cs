using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EEducationPlatform.Data;
using Volo.Abp.DependencyInjection;

namespace EEducationPlatform.EntityFrameworkCore;

public class EntityFrameworkCoreEEducationPlatformDbSchemaMigrator
    : IEEducationPlatformDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreEEducationPlatformDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the EEducationPlatformDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<EEducationPlatformDbContext>()
            .Database
            .MigrateAsync();
    }
}
