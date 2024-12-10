using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanSystem.Web.ViewModels;
using UrbanSystem.Common;

namespace UrbanSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = ValidationMessages.Error.PageNotFoundMessage;
                    ViewBag.ErrorCode = 404;
                    return View(ValidationMessages.Error.NotFoundView);
                default:
                    break;
            }

            return View(ValidationMessages.Error.ErrorView);
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ErrorMessage = ValidationMessages.Error.GeneralErrorMessage;
            ViewBag.ErrorCode = 500;

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
