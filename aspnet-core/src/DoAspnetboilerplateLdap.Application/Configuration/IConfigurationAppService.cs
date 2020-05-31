using System.Threading.Tasks;
using DoAspnetboilerplateLdap.Configuration.Dto;

namespace DoAspnetboilerplateLdap.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
