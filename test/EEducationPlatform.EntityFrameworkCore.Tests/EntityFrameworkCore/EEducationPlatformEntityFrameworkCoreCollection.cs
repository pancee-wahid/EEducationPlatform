using Xunit;

namespace EEducationPlatform.EntityFrameworkCore;

[CollectionDefinition(EEducationPlatformTestConsts.CollectionDefinitionName)]
public class EEducationPlatformEntityFrameworkCoreCollection : ICollectionFixture<EEducationPlatformEntityFrameworkCoreFixture>
{

}
