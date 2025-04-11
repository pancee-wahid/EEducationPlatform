using Volo.Abp.Modularity;

namespace EEducationPlatform;

[DependsOn(
    typeof(EEducationPlatformDomainModule),
    typeof(EEducationPlatformTestBaseModule)
)]
public class EEducationPlatformDomainTestModule : AbpModule
{

}
