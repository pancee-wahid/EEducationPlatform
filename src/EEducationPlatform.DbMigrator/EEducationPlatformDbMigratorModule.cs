using EEducationPlatform.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EEducationPlatform.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EEducationPlatformEntityFrameworkCoreModule),
    typeof(EEducationPlatformApplicationContractsModule)
)]
public class EEducationPlatformDbMigratorModule : AbpModule
{
}
