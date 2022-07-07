using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult Test()
        {
            return PartialView("~/Views/Analytics/_abcPartial.cshtml");
        }
    }

}
