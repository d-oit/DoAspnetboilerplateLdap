using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DoAspnetboilerplateLdap.EntityFrameworkCore
{
    public static class DoAspnetboilerplateLdapDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DoAspnetboilerplateLdapDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DoAspnetboilerplateLdapDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
