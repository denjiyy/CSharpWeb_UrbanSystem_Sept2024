using Microsoft.AspNetCore.Mvc;

namespace UrbanSystem.Web.Controllers
{
    public class FundingController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
