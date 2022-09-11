using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Services
{
    public class CalenderService : ICalender
    {
        public List<CalenderViewModel> GetCalenderModel(IEnumerable<CalenderDTO> calenderDTOs)
        {
            List<CalenderViewModel> viewModel = new List<CalenderViewModel>();

            foreach (CalenderDTO calenderDTO in calenderDTOs)
            {
                string title = calenderDTO.NumProducts.ToString();
                title = (title == "1") ? (title + " Product") : (title + " Products");
                string start = calenderDTO.FulfillmentDate.ToString("yyyy/MM/dd").Replace("/", "-");
                string url = "https://localhost:7020/Order/OrderDetails/" + calenderDTO.Id.ToString();
                viewModel.Add(new(title, start, "#3788d8", "#3788d8", url));
            }
            return viewModel;
        }
    }
}