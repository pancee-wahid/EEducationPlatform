using Volo.Abp.Modularity;

namespace EEducationPlatform;

/* Inherit from this class for your domain layer tests. */
public abstract class EEducationPlatformDomainTestBase<TStartupModule> : EEducationPlatformTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
