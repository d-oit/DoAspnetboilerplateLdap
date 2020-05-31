using System;
using System.Collections.Generic;
using System.Text;

namespace DoAspnetboilerplateLdap.EntityFrameworkCore
{
    public static class DbContextExtensions
    {
        public static void InsertOrUpdate(this DoAspnetboilerplateLdapDbContext context, object entity)
        {
            var existing = context.Entry(entity).IsKeySet;
            if (!existing)
            {
                context.Add(entity);
            }
            else
            {
                context.Entry(entity).CurrentValues.SetValues(entity);
            }
        }
    }
}
