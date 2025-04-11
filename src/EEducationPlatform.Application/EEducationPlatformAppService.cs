using EEducationPlatform.Localization;
using Volo.Abp.Application.Services;

namespace EEducationPlatform;

/* Inherit your application services from this class.
 */
public abstract class EEducationPlatformAppService : ApplicationService
{
    protected EEducationPlatformAppService()
    {
        LocalizationResource = typeof(EEducationPlatformResource);
    }
}
