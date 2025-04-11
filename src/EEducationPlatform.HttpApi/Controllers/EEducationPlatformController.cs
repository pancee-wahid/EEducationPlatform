using EEducationPlatform.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EEducationPlatform.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EEducationPlatformController : AbpControllerBase
{
    protected EEducationPlatformController()
    {
        LocalizationResource = typeof(EEducationPlatformResource);
    }
}
