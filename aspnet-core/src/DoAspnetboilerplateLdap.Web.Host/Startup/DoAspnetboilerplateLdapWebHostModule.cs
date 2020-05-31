using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DoAspnetboilerplateLdap.Configuration;

namespace DoAspnetboilerplateLdap.Web.Host.Startup
{
    [DependsOn(
       typeof(DoAspnetboilerplateLdapWebCoreModule))]
    public class DoAspnetboilerplateLdapWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DoAspnetboilerplateLdapWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DoAspnetboilerplateLdapWebHostModule).GetAssembly());
        }
    }
}
