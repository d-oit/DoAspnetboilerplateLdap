using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using DoAspnetboilerplateLdap.Configuration.Dto;

namespace DoAspnetboilerplateLdap.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DoAspnetboilerplateLdapAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
