using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.DTOs;

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
