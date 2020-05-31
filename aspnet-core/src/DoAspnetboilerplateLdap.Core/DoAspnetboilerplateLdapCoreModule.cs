using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap;
using Abp.Zero.Ldap.Configuration;
using DoAspnetboilerplateLdap.Authorization.Roles;
using DoAspnetboilerplateLdap.Authorization.Source.Ldap;
using DoAspnetboilerplateLdap.Authorization.Users;
using DoAspnetboilerplateLdap.Configuration;
using DoAspnetboilerplateLdap.Localization;
using DoAspnetboilerplateLdap.MultiTenancy;
using DoAspnetboilerplateLdap.Timing;

namespace DoAspnetboilerplateLdap
{
    [DependsOn(typeof(AbpZeroCoreModule), 
        typeof(AbpZeroLdapModule))]
    public class DoAspnetboilerplateLdapCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            DoAspnetboilerplateLdapLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = DoAspnetboilerplateLdapConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();

            Configuration.Modules.ZeroLdap().Enable(typeof(AppLdapAuthenticationSource));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DoAspnetboilerplateLdapCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
