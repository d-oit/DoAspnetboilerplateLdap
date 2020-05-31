using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Zero.Ldap.Configuration;
using Castle.Core.Logging;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Extensions;

namespace Abp.Zero.Ldap.Authentication
{
    /// <summary>
    /// Implements <see cref="IExternalAuthenticationSource{TTenant,TUser}"/> to authenticate users from LDAP.
    /// Extend this class using application's User and Tenant classes as type parameters.
    /// Also, all needed methods can be overridden and changed upon your needs.
    /// </summary>
    /// <typeparam name="TTenant">Tenant type</typeparam>
    /// <typeparam name="TUser">User type</typeparam>
    public abstract class LdapAuthenticationSource<TTenant, TUser> : DefaultExternalAuthenticationSource<TTenant, TUser>, ITransientDependency
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUserBase, new()
    {
        /// <summary>
        /// LDAP
        /// </summary>
        public const string SourceName = "LDAP";

        public override string Name => SourceName;

        private readonly ILdapSettings _settings;
        private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;
        private readonly ILogger _logger;

        protected LdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
        {
            _settings = settings;
            _ldapModuleConfig = ldapModuleConfig;
            //3: Do not write logs if no Logger supplied.
            _logger = NullLogger.Instance;
        }

        private Dictionary<string, string> LdapEntries;

        public Dictionary<string, string> GetLdapEntries()
        {
            return LdapEntries;
        }

        /// <inheritdoc/>
        public override async Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, TTenant tenant)
        {
            if (!_ldapModuleConfig.IsEnabled || !(await _settings.GetIsEnabled(tenant?.Id)))
            {
                return false;
            }

            if(await _settings.GetUseNovellLdap(tenant?.Id))
            {
                return await ValidateCredentialsAsync(userNameOrEmailAddress, tenant);
            }

            using (var principalContext = await CreatePrincipalContext(tenant, userNameOrEmailAddress))
            {
                return ValidateCredentials(principalContext, userNameOrEmailAddress, plainPassword);
            }
        }

        private async Task<bool> ValidateCredentialsAsync(string uid, TTenant tenant)
        {
            _logger.Info("ValidateCredentialsAsync against ldap host");
            int ldapPort = await _settings.GetLdapServerPort(tenant?.Id);
            string ldapHost = await _settings.GetLdapHost(tenant?.Id);
            var loginDN = await _settings.GetLdapLoginDn(tenant?.Id); 
            var loginPassword = await _settings.GetPassword(tenant?.Id);
            var ldapSearchBase = await _settings.GetLdapUserSearchBase(tenant?.Id);

            string searchLdapUser = uid;

            string searchFilter = "(objectclass=*)";
            string searchBase = $"uid={searchLdapUser}, {ldapSearchBase}"; // "ou = scientists, dc = example, dc = com"; //"uid=gauss, dc=example, dc=com"; 

            LdapSearchConstraints constraints = new LdapSearchConstraints { };

            try
            {
                using (var cn = new LdapConnection())
                {
                    // connect
                    cn.Connect(ldapHost, ldapPort);
                    cn.Bind(loginDN, loginPassword);
                       
                    LdapSearchResults searchResults = cn.Search(
                       searchBase,
                       LdapConnection.SCOPE_SUB,
                       searchFilter,
                       null, // no specified attributes
                       false, // false = return attr and value
                       constraints);


                    while (searchResults.HasMore())
                    {
                        if (searchResults.Count == 1)
                        {
                            LdapEntry nextEntry = null;
                            try
                            {
                                nextEntry = searchResults.Next();
                            }
                            catch (LdapException e)
                            {
                                _logger.Error("Error: " + e.LdapErrorMessage);
                                //Exception is thrown, go for next entry
                                continue;
                            }

                            LdapEntries = new Dictionary<string, string>();

                            _logger.Debug(nextEntry.DN);

                            // Get the attribute set of the entry
                            LdapAttributeSet attributeSet = nextEntry.getAttributeSet();
                            System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();

                            // Parse through the attribute set to get the attributes and the corresponding values
                            while (ienum.MoveNext())
                            {
                                LdapAttribute attribute = (LdapAttribute)ienum.Current;
                                string attributeName = attribute.Name;
                                string attributeVal = attribute.StringValue;
                                _logger.Debug(attributeName + "value:" + attributeVal);
                                LdapEntries.Add(attributeName, attributeVal);
                            }
                            return true;
                        }
                    }
                }
            }
            catch (LdapException ldapEx)
            {
                throw new AbpException(ldapEx.ToString()); // ocassional time outs
            }
            catch (Exception ex)
            {
                throw new AbpException(ex.ToString());
            }
            return false;
        }

        /// <inheritdoc/>
        public override async Task<TUser> CreateUserAsync(string userNameOrEmailAddress, TTenant tenant)
        {
            await CheckIsEnabled(tenant);

            var user = await base.CreateUserAsync(userNameOrEmailAddress, tenant);
            if (await _settings.GetUseNovellLdap(tenant?.Id))
            {
                return user;
            }

            using (var principalContext = await CreatePrincipalContext(tenant, user))
            {
                var userPrincipal = UserPrincipal.FindByIdentity(principalContext, userNameOrEmailAddress);

                if (userPrincipal == null)
                {
                    throw new AbpException("Unknown LDAP user: " + userNameOrEmailAddress);
                }

                UpdateUserFromPrincipal(user, userPrincipal);

                user.IsEmailConfirmed = true;
                user.IsActive = true;

                return user;
            }
        }

        public override async Task UpdateUserAsync(TUser user, TTenant tenant)
        {
            await CheckIsEnabled(tenant);

            await base.UpdateUserAsync(user, tenant);

            using (var principalContext = await CreatePrincipalContext(tenant, user))
            {
                var userPrincipal = UserPrincipal.FindByIdentity(principalContext, user.UserName);

                if (userPrincipal == null)
                {
                    throw new AbpException("Unknown LDAP user: " + user.UserName);
                }

                UpdateUserFromPrincipal(user, userPrincipal);
            }
        }

        protected virtual bool ValidateCredentials(PrincipalContext principalContext, string userNameOrEmailAddress, string plainPassword)
        {
            return principalContext.ValidateCredentials(userNameOrEmailAddress, plainPassword, ContextOptions.Negotiate);
        }

        protected virtual void UpdateUserFromPrincipal(TUser user, UserPrincipal userPrincipal)
        {
            if (!userPrincipal.SamAccountName.IsNullOrEmpty())
            {
                user.UserName = userPrincipal.SamAccountName;
            }
            
            user.Name = userPrincipal.GivenName;
            user.Surname = userPrincipal.Surname;
            user.EmailAddress = userPrincipal.EmailAddress;

            if (userPrincipal.Enabled.HasValue)
            {
                user.IsActive = userPrincipal.Enabled.Value;
            }
        }


        protected virtual Task<PrincipalContext> CreatePrincipalContext(TTenant tenant, string userNameOrEmailAddress)
        {
            return CreatePrincipalContext(tenant);
        }

        protected virtual Task<PrincipalContext> CreatePrincipalContext(TTenant tenant, TUser user)
        {
            return CreatePrincipalContext(tenant);
        }

        protected virtual async Task<PrincipalContext> CreatePrincipalContext(TTenant tenant)
        {

            return new PrincipalContext(
                await _settings.GetContextType(tenant?.Id),
                ConvertToNullIfEmpty(await _settings.GetDomain(tenant?.Id)),
                ConvertToNullIfEmpty(await _settings.GetContainer(tenant?.Id)),
                ConvertToNullIfEmpty(await _settings.GetUserName(tenant?.Id)),
                ConvertToNullIfEmpty(await _settings.GetPassword(tenant?.Id))
            );
        }

        protected virtual async Task CheckIsEnabled(TTenant tenant)
        {
            if (!_ldapModuleConfig.IsEnabled)
            {
                throw new AbpException("Ldap Authentication module is disabled globally!");                
            }

            var tenantId = tenant?.Id;
            if (!await _settings.GetIsEnabled(tenantId))
            {
                throw new AbpException("Ldap Authentication is disabled for given tenant (id:" + tenantId + ")! You can enable it by setting '" + LdapSettingNames.IsEnabled + "' to true");
            }
        }

        protected static string ConvertToNullIfEmpty(string str)
        {
            return str.IsNullOrWhiteSpace()
                ? null
                : str;
        }
    }
}
