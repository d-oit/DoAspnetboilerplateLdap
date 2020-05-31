using Abp.AspNetCore.Mvc.ViewComponents;

namespace DoAspnetboilerplateLdap.Web.Views
{
    public abstract class DoAspnetboilerplateLdapViewComponent : AbpViewComponent
    {
        protected DoAspnetboilerplateLdapViewComponent()
        {
            LocalizationSourceName = DoAspnetboilerplateLdapConsts.LocalizationSourceName;
        }
    }
}
