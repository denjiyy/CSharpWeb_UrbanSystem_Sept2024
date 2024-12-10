using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanSystem.Common;
using UrbanSystem.Web.ViewModels;

namespace UrbanSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewData[ValidationMessages.Home.TitleKey] = ValidationMessages.Home.TitleValue;
            ViewData[ValidationMessages.Home.MessageKey] = ValidationMessages.Home.WelcomeMessage;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}