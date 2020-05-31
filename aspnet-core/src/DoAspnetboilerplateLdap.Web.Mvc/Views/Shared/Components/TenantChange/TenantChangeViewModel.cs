using Abp.AutoMapper;
using DoAspnetboilerplateLdap.Sessions.Dto;

namespace DoAspnetboilerplateLdap.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
