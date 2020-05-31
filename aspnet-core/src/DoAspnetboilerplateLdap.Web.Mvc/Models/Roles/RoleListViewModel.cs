using System.Collections.Generic;
using DoAspnetboilerplateLdap.Roles.Dto;

namespace DoAspnetboilerplateLdap.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
