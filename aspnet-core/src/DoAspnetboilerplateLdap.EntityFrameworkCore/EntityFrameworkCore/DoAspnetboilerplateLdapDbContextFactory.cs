using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DoAspnetboilerplateLdap.Configuration;
using DoAspnetboilerplateLdap.Web;

namespace DoAspnetboilerplateLdap.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DoAspnetboilerplateLdapDbContextFactory : IDesignTimeDbContextFactory<DoAspnetboilerplateLdapDbContext>
    {
        public DoAspnetboilerplateLdapDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DoAspnetboilerplateLdapDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DoAspnetboilerplateLdapDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DoAspnetboilerplateLdapConsts.ConnectionStringName));

            return new DoAspnetboilerplateLdapDbContext(builder.Options);
        }
    }
}
