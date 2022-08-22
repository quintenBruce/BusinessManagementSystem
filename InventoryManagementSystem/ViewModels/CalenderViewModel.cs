using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;

namespace InventoryManagementSystem.ViewModels
{
    public class CalenderViewModel
    {
        public string title { get; set; }
        public string start { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string url { get; set; }

        public CalenderViewModel(string _title, string _start, string _backgroundColor, string _borderColor, string _url)
        {
            title = _title;
            start = _start;
            backgroundColor = _backgroundColor;
            borderColor = _borderColor;
            url = _url;
        }
        public CalenderViewModel()
        {

        }

       
    }
}
