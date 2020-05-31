using System.Threading.Tasks;
using Abp.Application.Services;
using DoAspnetboilerplateLdap.Sessions.Dto;

namespace DoAspnetboilerplateLdap.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
