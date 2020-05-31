using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.MultiTenancy;
using DoAspnetboilerplateLdap.Editions;
using DoAspnetboilerplateLdap.MultiTenancy;

namespace DoAspnetboilerplateLdap.EntityFrameworkCore.Seed.Tenants
{
    public class TestTenantBuilder
    {
        private readonly DoAspnetboilerplateLdapDbContext _context;

        public TestTenantBuilder(DoAspnetboilerplateLdapDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateTestTenant();
        }

        private void CreateTestTenant()
        {
            // Test
            var testTenantName = "TEST";

            var testTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == testTenantName);
            if (testTenant == null)
            {
                testTenant = new Tenant(testTenantName, testTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    testTenant.EditionId = defaultEdition.Id;
                }
            }
            _context.InsertOrUpdate(testTenant);
            _context.SaveChanges();
        }
        
    }
}
