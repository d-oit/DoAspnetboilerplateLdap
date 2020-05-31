using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace DoAspnetboilerplateLdap.Web.Views
{
    public abstract class DoAspnetboilerplateLdapRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected DoAspnetboilerplateLdapRazorPage()
        {
            LocalizationSourceName = DoAspnetboilerplateLdapConsts.LocalizationSourceName;
        }
    }
}
