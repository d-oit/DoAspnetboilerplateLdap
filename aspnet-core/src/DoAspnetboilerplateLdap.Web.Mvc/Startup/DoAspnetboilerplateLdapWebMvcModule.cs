using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DoAspnetboilerplateLdap.Configuration;

namespace DoAspnetboilerplateLdap.Web.Startup
{
    [DependsOn(typeof(DoAspnetboilerplateLdapWebCoreModule))]
    public class DoAspnetboilerplateLdapWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DoAspnetboilerplateLdapWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<DoAspnetboilerplateLdapNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DoAspnetboilerplateLdapWebMvcModule).GetAssembly());
        }
    }
}
