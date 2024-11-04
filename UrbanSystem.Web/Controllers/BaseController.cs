using Microsoft.AspNetCore.Mvc;

namespace UrbanSystem.Web.Controllers
{
	public class BaseController : Controller
	{
		protected bool IsGuidIdValid(string? id, ref Guid locationGuid)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return false;
			}

			bool isGuidValid = Guid.TryParse(id, out locationGuid);
			if (isGuidValid)
			{
				return false;
			}

			return true;
		}
	}
}
