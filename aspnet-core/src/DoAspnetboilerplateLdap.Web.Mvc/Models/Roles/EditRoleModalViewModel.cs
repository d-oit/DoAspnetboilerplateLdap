using Abp.AutoMapper;
using DoAspnetboilerplateLdap.Roles.Dto;
using DoAspnetboilerplateLdap.Web.Models.Common;

namespace DoAspnetboilerplateLdap.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
