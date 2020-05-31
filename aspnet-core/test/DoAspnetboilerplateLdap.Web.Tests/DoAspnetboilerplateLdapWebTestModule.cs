using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DoAspnetboilerplateLdap.EntityFrameworkCore;
using DoAspnetboilerplateLdap.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace DoAspnetboilerplateLdap.Web.Tests
{
    [DependsOn(
        typeof(DoAspnetboilerplateLdapWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class DoAspnetboilerplateLdapWebTestModule : AbpModule
    {
        public DoAspnetboilerplateLdapWebTestModule(DoAspnetboilerplateLdapEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DoAspnetboilerplateLdapWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(DoAspnetboilerplateLdapWebMvcModule).Assembly);
        }
    }
}