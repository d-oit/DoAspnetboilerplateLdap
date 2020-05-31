using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace DoAspnetboilerplateLdap.Controllers
{
    public abstract class DoAspnetboilerplateLdapControllerBase: AbpController
    {
        protected DoAspnetboilerplateLdapControllerBase()
        {
            LocalizationSourceName = DoAspnetboilerplateLdapConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
