using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using DoAspnetboilerplateLdap.Authorization.Roles;
using DoAspnetboilerplateLdap.Authorization.Users;
using DoAspnetboilerplateLdap.MultiTenancy;
using Abp.Domain.Entities;

namespace DoAspnetboilerplateLdap.EntityFrameworkCore
{
    public class DoAspnetboilerplateLdapDbContext : AbpZeroDbContext<Tenant, Role, User, DoAspnetboilerplateLdapDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DoAspnetboilerplateLdapDbContext(DbContextOptions<DoAspnetboilerplateLdapDbContext> options)
            : base(options)
        {


        }

        

    }
}
