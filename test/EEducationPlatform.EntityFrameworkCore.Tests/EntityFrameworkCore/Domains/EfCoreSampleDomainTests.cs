using EEducationPlatform.Samples;
using Xunit;

namespace EEducationPlatform.EntityFrameworkCore.Domains;

[Collection(EEducationPlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EEducationPlatformEntityFrameworkCoreTestModule>
{

}
