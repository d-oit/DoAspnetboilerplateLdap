using System.Collections.Generic;

namespace DoAspnetboilerplateLdap.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
