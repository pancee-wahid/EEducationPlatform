using Volo.Abp.Modularity;

namespace EEducationPlatform;

public abstract class EEducationPlatformApplicationTestBase<TStartupModule> : EEducationPlatformTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
