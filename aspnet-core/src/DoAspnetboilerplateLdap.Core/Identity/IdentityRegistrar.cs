using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using DoAspnetboilerplateLdap.Authorization;
using DoAspnetboilerplateLdap.Authorization.Roles;
using DoAspnetboilerplateLdap.Authorization.Users;
using DoAspnetboilerplateLdap.Editions;
using DoAspnetboilerplateLdap.MultiTenancy;

namespace DoAspnetboilerplateLdap.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
