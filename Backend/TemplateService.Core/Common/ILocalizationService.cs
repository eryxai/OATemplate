using Framework.Common.Enums;

namespace TemplateService.Core.Common
{
    public interface ILocalizationService
    {
        string GetTranslate(string key, Language language);
    }
}
