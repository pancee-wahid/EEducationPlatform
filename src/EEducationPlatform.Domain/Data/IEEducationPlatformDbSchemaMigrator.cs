using System.Threading.Tasks;

namespace EEducationPlatform.Data;

public interface IEEducationPlatformDbSchemaMigrator
{
    Task MigrateAsync();
}
