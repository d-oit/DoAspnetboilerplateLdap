using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using DoAspnetboilerplateLdap.Authorization.Roles;
using DoAspnetboilerplateLdap.Authorization.Users;
using DoAspnetboilerplateLdap.MultiTenancy;
using Microsoft.Extensions.Logging;

namespace DoAspnetboilerplateLdap.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager signInManager,
            ISystemClock systemClock,
            ILoggerFactory loggerFactory) 
            : base(options, signInManager, systemClock, loggerFactory)
        {
        }
    }
}
