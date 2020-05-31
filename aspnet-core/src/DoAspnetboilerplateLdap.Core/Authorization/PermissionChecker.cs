using Abp.Authorization;
using DoAspnetboilerplateLdap.Authorization.Roles;
using DoAspnetboilerplateLdap.Authorization.Users;

namespace DoAspnetboilerplateLdap.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
