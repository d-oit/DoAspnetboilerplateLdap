using System.Collections.Generic;
using DoAspnetboilerplateLdap.Roles.Dto;

namespace DoAspnetboilerplateLdap.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}