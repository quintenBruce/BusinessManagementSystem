using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class CalenderController : Controller
    {
        private readonly ICalender _calenderService;
        private WebApiService _webApiService;

        public CalenderController(ICalender calenderService, WebApiService webApiService)
        {
            _calenderService = calenderService;
            _webApiService = webApiService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CalenderDTO> calenderDTOs = await _webApiService.GetCalenderDTOs();
            var ViewModel = _calenderService.GetCalenderModel(calenderDTOs);
            return View(ViewModel);
        }
    }
}