using Abp.Configuration;
using Abp.Localization;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace Abp.Zero.Ldap.Configuration
{
    /// <summary>
    /// Defines LDAP settings.
    /// </summary>
    public class LdapSettingProvider : SettingProvider
    {
        protected string LocalizationSourceName { get; set; }

        public LdapSettingProvider()
        {
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(LdapSettingNames.IsEnabled, "false", L("Ldap_IsEnabled"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.ContextType, ContextType.Domain.ToString(), L("Ldap_ContextType"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.Container, null, L("Ldap_Container"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.Domain, null, L("Ldap_Domain"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.UserName, null, L("Ldap_UserName"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.Password, null, L("Ldap_Password"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.LdapServer,  null, L("Ldap_Server"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false ),
                       new SettingDefinition(LdapSettingNames.UseNovellLdap,  null, L("Ldap_UseNovellLdap"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false ),
                       new SettingDefinition(LdapSettingNames.LdapPort,  null, L("Ldap_LdapPort"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false ),
                       new SettingDefinition(LdapSettingNames.LdapSearchConstraintTimeout,  null, L("Ldap_LdapSearchConstraintTimeout"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false ),
                       new SettingDefinition(LdapSettingNames.LdapLoginDn,  null, L("Ldap_LdapLoginDn"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false ),
                       new SettingDefinition(LdapSettingNames.LdapSearchBaseDn,  null, L("Ldap_LdapSearchBaseDn"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false ),
                    };
        }

        protected virtual ILocalizableString L(string name)
        {
            return new LocalizableString(name, LocalizationSourceName);
        }
    }
}