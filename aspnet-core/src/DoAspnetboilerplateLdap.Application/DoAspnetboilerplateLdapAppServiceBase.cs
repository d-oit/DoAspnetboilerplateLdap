using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using DoAspnetboilerplateLdap.Authorization.Users;
using DoAspnetboilerplateLdap.MultiTenancy;

namespace DoAspnetboilerplateLdap
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class DoAspnetboilerplateLdapAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected DoAspnetboilerplateLdapAppServiceBase()
        {
            LocalizationSourceName = DoAspnetboilerplateLdapConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
