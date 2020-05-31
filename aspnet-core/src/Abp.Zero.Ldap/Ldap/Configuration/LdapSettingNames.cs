namespace Abp.Zero.Ldap.Configuration
{
    /// <summary>
    /// Contains consts for LDAP setting names.
    /// </summary>
    public static class LdapSettingNames
    {
        /// <summary>
        /// Abp.Zero.Ldap.IsEnabled
        /// </summary>
        public const string IsEnabled = "Abp.Zero.Ldap.IsEnabled";

        /// <summary>
        /// Abp.Zero.Ldap.ContextType
        /// </summary>
        public const string ContextType = "Abp.Zero.Ldap.ContextType";
        
        /// <summary>
        /// Abp.Zero.Ldap.Container
        /// </summary>
        public const string Container = "Abp.Zero.Ldap.Container";
        
        /// <summary>
        /// Abp.Zero.Ldap.Domain
        /// </summary>
        public const string Domain = "Abp.Zero.Ldap.Domain";
        
        /// <summary>
        /// Abp.Zero.Ldap.UserName
        /// </summary>
        public const string UserName = "Abp.Zero.Ldap.UserName";
        
        /// <summary>
        /// Abp.Zero.Ldap.Password
        /// </summary>
        public const string Password = "Abp.Zero.Ldap.Password";

        /// <summary>
        /// Abp.Zero.Ldap.UseDirectoryServices
        /// </summary>
        public const string UseNovellLdap = "Abp.Zero.Ldap.UseNovellLdap";

        /// <summary>
        /// Abp.Zero.Ldap.AuthenticationTypes
        /// </summary>
        public const string LdapPort = "Abp.Zero.Ldap.LdapPort";

        /// <summary>
        /// Abp.Zero.Ldap.LdapServer
        /// e.g. LDAP://ldap.forumsys.com:389/dc=example,dc=com
        /// </summary>
        public const string LdapServer = "Abp.Zero.Ldap.LdapServer";

        public const string LdapLoginDn = "Abp.Zero.Ldap.LdapLoginDn";

        public const string LdapSearchConstraintTimeout = "Abp.Zero.Ldap.SearchConstraintTimeout";

        public const string LdapSearchBaseDn = "Abp.Zero.Ldap.LdapSearchBaseDn";

    }
}