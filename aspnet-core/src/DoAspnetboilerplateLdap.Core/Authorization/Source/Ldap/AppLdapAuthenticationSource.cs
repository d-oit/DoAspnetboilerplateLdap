using Abp;
using Abp.Configuration;
using Abp.UI;
using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using DoAspnetboilerplateLdap.Authorization.Users;
using DoAspnetboilerplateLdap.MultiTenancy;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace DoAspnetboilerplateLdap.Authorization.Source.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        private readonly ILdapSettings _settings;

        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig) : base(settings, ldapModuleConfig)
        {
            _settings = settings;
            
        }

        public override string Name => "OpenLdap";

        public override Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, Tenant tenant)
        {
            try
            {
                return base.TryAuthenticateAsync(userNameOrEmailAddress, plainPassword, tenant);
            }
            catch (System.Exception e)
            {
                throw new UserFriendlyException("Ldap Server error: " + e.Message);
            }
        }

        public override async Task<User> CreateUserAsync(string userNameOrEmailAddress, Tenant tenant)
        {
            if(await _settings.GetUseNovellLdap(tenant?.Id))
            {
                var user = await base.CreateUserAsync(userNameOrEmailAddress, tenant);
                UpdateUser(user);
                return user;
            }

            return await base.CreateUserAsync(userNameOrEmailAddress, tenant);
        }

        public override async Task UpdateUserAsync(User user, Tenant tenant)
        {
            if (await _settings.GetUseNovellLdap(tenant?.Id))
            {
                UpdateUser(user);
                return;
            }

            await base.UpdateUserAsync(user, tenant);
        }

        // Update IdentityUser with attributes from  LdapEntries
        protected void UpdateUser(User user)
        {
            var ldapEntries = GetLdapEntries();
            if (ldapEntries == null) return;
            foreach (var item in ldapEntries.Keys)
            {
                switch (item.ToLower())
                {
                    case "uidvalue":
                        user.UserName = ldapEntries[item];
                        user.IsActive = true;
                        user.IsDeleted = false;
                        user.IsEmailConfirmed = true;
                        break;
                    case "cnvalue":
                        user.Name = ldapEntries[item];
                        break;
                    case "mailvalue":
                        user.EmailAddress = ldapEntries[item];
                        break;
                    default:
                        break;
                }
            }

            // e.g.

            // uid = riemann,dc = example,dc = com
            // mailvalue: riemann @ldap.forumsys.com
            // cnvalue:Bernhard Riemann
            // uidvalue: riemann
            // snvalue:Riemann
            // objectClassvalue:inetOrgPerson
        }

    }
}