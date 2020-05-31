using Abp.AutoMapper;
using DoAspnetboilerplateLdap.Authentication.External;

namespace DoAspnetboilerplateLdap.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
