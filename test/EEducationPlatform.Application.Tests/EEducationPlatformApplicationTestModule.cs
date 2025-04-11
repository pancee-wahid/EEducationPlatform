using Volo.Abp.Modularity;

namespace EEducationPlatform;

[DependsOn(
    typeof(EEducationPlatformApplicationModule),
    typeof(EEducationPlatformDomainTestModule)
)]
public class EEducationPlatformApplicationTestModule : AbpModule
{

}
