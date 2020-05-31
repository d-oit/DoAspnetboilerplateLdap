using System.Threading.Tasks;
using DoAspnetboilerplateLdap.Models.TokenAuth;
using DoAspnetboilerplateLdap.Web.Controllers;
using Shouldly;
using Xunit;

namespace DoAspnetboilerplateLdap.Web.Tests.Controllers
{
    public class HomeController_Tests: DoAspnetboilerplateLdapWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}