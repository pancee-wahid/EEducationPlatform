using EEducationPlatform.Samples;
using Xunit;

namespace EEducationPlatform.EntityFrameworkCore.Applications;

[Collection(EEducationPlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EEducationPlatformEntityFrameworkCoreTestModule>
{

}
