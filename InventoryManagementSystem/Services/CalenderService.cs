using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Services
{
    public class CalenderService : ICalender
    {
        private readonly IOrder orderService;
        private readonly IProduct productService;


        public CalenderService(IOrder _ordersService, IProduct _productService)
        {
            orderService = _ordersService;
            productService = _productService;
        }
        public List<CalenderViewModel> GetCalenderModel()
        {
            List<CalenderViewModel> viewModel = new List<CalenderViewModel>();
            List<Order> orders = orderService.GetOrders();
            List<Product> products = productService.GetProducts();

            foreach (Order order in orders.Where(x => x.Status == false))
            {
                string title = products.Where(x => x.Order.Id == order.Id).ToList().Count.ToString();
                title = (title == "1") ? (title + " Product") : (title + " Products");
                string start = order.FulfillmentDate.ToString("yyyy/MM/dd").Replace("/", "-");
                string url = "https://localhost:44306/Order/OrderDetails/" + order.Id.ToString();
                CalenderViewModel model = new(title, start, "#3788d8", "#3788d8", url);
                viewModel.Add(model);
            }
            return viewModel;

        }
    }
}
