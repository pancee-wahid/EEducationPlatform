using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EEducationPlatform.Data;

/* This is used if database provider does't define
 * IEEducationPlatformDbSchemaMigrator implementation.
 */
public class NullEEducationPlatformDbSchemaMigrator : IEEducationPlatformDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
