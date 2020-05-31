using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace DoAspnetboilerplateLdap.Localization
{
    public static class DoAspnetboilerplateLdapLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(DoAspnetboilerplateLdapConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DoAspnetboilerplateLdapLocalizationConfigurer).GetAssembly(),
                        "DoAspnetboilerplateLdap.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
