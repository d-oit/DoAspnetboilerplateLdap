using Abp.Application.Services;
using DoAspnetboilerplateLdap.MultiTenancy.Dto;

namespace DoAspnetboilerplateLdap.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

