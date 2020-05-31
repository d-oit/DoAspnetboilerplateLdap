using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using DoAspnetboilerplateLdap.Controllers;

namespace DoAspnetboilerplateLdap.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : DoAspnetboilerplateLdapControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
