using Framework.Common.Enums;
using TemplateService.Core.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace TemplateService.Business.Common
{
    public class LocalizationService : ILocalizationService
    {
        private Dictionary<string, string> localizationArabicDictionary;
        private Dictionary<string, string> localizationEnglishDictionary;
        public LocalizationService()
        {
            string rootDictionarypath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string localizationArabicDictionarypath = $"{rootDictionarypath}\\Localization\\Template-ar.xml";
            string localizationEnglishDictionarypath = $"{rootDictionarypath}\\Localization\\Template-en.xml";
            XDocument arabicdocument = XDocument.Load(localizationArabicDictionarypath);
            localizationArabicDictionary = new Dictionary<string, string>();

            XDocument englishdocument = XDocument.Load(localizationEnglishDictionarypath);
            localizationEnglishDictionary = new Dictionary<string, string>();

            foreach (XElement element in arabicdocument.Descendants().Where(p => p.HasElements == false))
            {
                int keyInt = 0;
                string keyName = element.Name.LocalName;

                while (localizationArabicDictionary.ContainsKey(keyName))
                {
                    keyName = element.Name.LocalName + "_" + keyInt++;
                }

                localizationArabicDictionary.Add(keyName, element.Value);
            }


            foreach (XElement element in englishdocument.Descendants().Where(p => p.HasElements == false))
            {
                int keyInt = 0;
                string keyName = element.Name.LocalName;

                while (localizationEnglishDictionary.ContainsKey(keyName))
                {
                    keyName = element.Name.LocalName + "_" + keyInt++;
                }

                localizationEnglishDictionary.Add(keyName, element.Value);
            }
        }

        public string GetTranslate(string key, Language language)
        {
            if (language == Language.Arabic)
            {
                return localizationArabicDictionary[key];
            }
            else
            {
                return localizationEnglishDictionary[key];
            }
        }
    }
}
