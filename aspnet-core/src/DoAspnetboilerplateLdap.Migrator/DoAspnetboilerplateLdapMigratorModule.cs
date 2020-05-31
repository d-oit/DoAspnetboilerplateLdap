using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DoAspnetboilerplateLdap.Configuration;
using DoAspnetboilerplateLdap.EntityFrameworkCore;
using DoAspnetboilerplateLdap.Migrator.DependencyInjection;

namespace DoAspnetboilerplateLdap.Migrator
{
    [DependsOn(typeof(DoAspnetboilerplateLdapEntityFrameworkModule))]
    public class DoAspnetboilerplateLdapMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public DoAspnetboilerplateLdapMigratorModule(DoAspnetboilerplateLdapEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(DoAspnetboilerplateLdapMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                DoAspnetboilerplateLdapConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DoAspnetboilerplateLdapMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
