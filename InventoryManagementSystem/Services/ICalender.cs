using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Services
{
    public interface ICalender
    {
        List<CalenderViewModel> GetCalenderModel(IEnumerable<CalenderDTO> calenderDTOs);
    }
}