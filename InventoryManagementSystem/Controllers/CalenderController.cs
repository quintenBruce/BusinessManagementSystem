using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Services;

namespace InventoryManagementSystem.Controllers
{
    public class CalenderController : Controller
    {
        private readonly ICalender _calenderService;


        public CalenderController(ICalender calenderService)
        {
            _calenderService = calenderService;
        }

        public IActionResult Index()
        {
            CalenderViewModel model = new CalenderViewModel();
            var ViewModel = _calenderService.GetCalenderModel();
            return View(ViewModel);
        }
    }
}
