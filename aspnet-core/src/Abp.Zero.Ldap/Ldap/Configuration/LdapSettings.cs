using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Extensions;

namespace Abp.Zero.Ldap.Configuration
{
    /// <summary>
    /// Implements <see cref="ILdapSettings"/> to get settings from <see cref="ISettingManager"/>.
    /// </summary>
    
    public class LdapSettings : ILdapSettings, ITransientDependency
    {
        protected ISettingManager SettingManager { get; }

        public LdapSettings(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        public virtual Task<bool> GetIsEnabled(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync<bool>(LdapSettingNames.IsEnabled, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync<bool>(LdapSettingNames.IsEnabled);
        }

        public virtual async Task<ContextType> GetContextType(int? tenantId)
        {
            return tenantId.HasValue
                ? (await SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.ContextType, tenantId.Value)).ToEnum<ContextType>()
                : (await SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.ContextType)).ToEnum<ContextType>();
        }

        public virtual Task<string> GetContainer(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Container, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Container);
        }

        public virtual Task<string> GetDomain(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Domain, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Domain);
        }

        public virtual Task<string> GetUserName(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.UserName, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.UserName);
        }

        public virtual Task<string> GetPassword(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Password, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Password);
        }
       

        public virtual Task<bool> GetUseNovellLdap(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync<bool>(LdapSettingNames.UseNovellLdap, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync<bool>(LdapSettingNames.UseNovellLdap);
        }

        public virtual Task<string> GetLdapHost(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.LdapServer, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.LdapServer);
        }

        public virtual async Task<int> GetLdapServerPort(int? tenantId)
        {
            var portString = tenantId.HasValue
                ? (await SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.LdapPort, tenantId.Value))
                : (await SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.LdapPort));

            int port = Novell.Directory.Ldap.LdapConnection.DEFAULT_PORT;
            if (portString != null)
            {
                port = Convert.ToInt32(portString);
            }
            return port;
        }


        public virtual Task<string> GetLdapLoginDn(int? tenantId)
        {
            return tenantId.HasValue
               ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.LdapLoginDn, tenantId.Value)
               : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.LdapLoginDn);
        }

        public virtual Task<string> GetLdapUserSearchBase(int? tenantId)
        {
            return tenantId.HasValue
               ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.LdapSearchBaseDn, tenantId.Value)
               : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.LdapSearchBaseDn);
        }
     
    }
}