using Abp.MultiTenancy;
using DoAspnetboilerplateLdap.Authorization.Users;

namespace DoAspnetboilerplateLdap.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
