using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Abp.Zero.Ldap.Configuration;

namespace DoAspnetboilerplateLdap.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly DoAspnetboilerplateLdapDbContext _context;

        public DefaultSettingsCreator(DoAspnetboilerplateLdapDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            int? tenantId = null;

            if (DoAspnetboilerplateLdapConsts.MultiTenancyEnabled == false)
            {
                tenantId = MultiTenancyConsts.DefaultTenantId;
            }

            // Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com", tenantId);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer", tenantId);

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en", tenantId);


            // Ldap / Ad
            AddSettingIfNotExists(LdapSettingNames.IsEnabled, "true", tenantId);

            // *** AD ***
            // Setup the domain - not necassery if your website service user could login to your configured windwos domain
            //AddSettingIfNotExists(LdapSettingNames.UserName, "AD_Administrator", tenantId);
            //AddSettingIfNotExists(LdapSettingNames.Password, "password", tenantId);
            //AddSettingIfNotExists(LdapSettingNames.ContextType, "Domain.Context", tenantId);
            //AddSettingIfNotExists(LdapSettingNames.Domain, "test.local");

            // *** LDAP Sever ***

            //https://www.forumsys.com/tutorials/integration-how-to/ldap/online-ldap-test-server/

            AddSettingIfNotExists(LdapSettingNames.UseNovellLdap, "true", tenantId);
            AddSettingIfNotExists(LdapSettingNames.LdapServer, "ldap.forumsys.com", tenantId);
            // Ldap admin user
            AddSettingIfNotExists(LdapSettingNames.LdapLoginDn, "cn=read-only-admin,dc=example,dc=com", tenantId);
            AddSettingIfNotExists(LdapSettingNames.Password, "password", tenantId);

            AddSettingIfNotExists(LdapSettingNames.LdapSearchBaseDn, "dc=example,dc=com", tenantId);
        }



        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
