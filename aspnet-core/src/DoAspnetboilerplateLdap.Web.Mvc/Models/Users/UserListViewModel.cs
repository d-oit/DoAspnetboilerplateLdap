using System.Collections.Generic;
using DoAspnetboilerplateLdap.Roles.Dto;

namespace DoAspnetboilerplateLdap.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
