using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        public async Task<ViewResult> Index()
        {

            ICategory categoryInterface = new CategoryService();
            var retrievedCategories = await categoryInterface.GetAllCategories();

            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.categories = retrievedCategories;
            
            return View(orderViewModel);
        }

        [HttpPost]
        public ActionResult Index(OrderViewModel orderViewModel)
        {
            ICategory categoryService = new CategoryService();
            var retrievedCategories = categoryService.GetAllCategories();


            for (int i = 0; i < orderViewModel.products.Count(); i++)
            {
                //orderViewModel.products[i].Category = retrievedCategories.Result.Where(x => x.Id == orderViewModel.categoryIds[i]).First();
                orderViewModel.products[i].Order = orderViewModel.order;
                orderViewModel.order.Price += orderViewModel.products[i].Price;
            }






            //customerService.CreateCustomer(orderViewModel.customer);
           

            //orderViewModel.order.Customer = orderViewModel.customer;
            if (orderViewModel.paymentHistory != null)
                orderViewModel.paymentHistory.Order = orderViewModel.order;
            



            orderViewModel.order.Order_date = DateTime.Now; 
            IOrderGroup orderGroupService = new OrderGroupService();
            orderGroupService.CreateOrderGroup(orderViewModel.products, orderViewModel.order, orderViewModel.paymentHistory, orderViewModel.customer, orderViewModel.categoryIds);



            



            
            


            return RedirectToAction("Index");
        }
    }
}
