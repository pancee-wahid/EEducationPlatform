using Microsoft.Extensions.Localization;
using EEducationPlatform.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EEducationPlatform;

[Dependency(ReplaceServices = true)]
public class EEducationPlatformBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<EEducationPlatformResource> _localizer;

    public EEducationPlatformBrandingProvider(IStringLocalizer<EEducationPlatformResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
