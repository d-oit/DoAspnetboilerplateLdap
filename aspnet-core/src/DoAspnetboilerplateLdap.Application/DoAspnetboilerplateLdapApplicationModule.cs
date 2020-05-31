using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DoAspnetboilerplateLdap.Authorization;

namespace DoAspnetboilerplateLdap
{
    [DependsOn(
        typeof(DoAspnetboilerplateLdapCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class DoAspnetboilerplateLdapApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DoAspnetboilerplateLdapAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DoAspnetboilerplateLdapApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
