using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Services;

namespace InventoryManagementSystem.Controllers
{
    public class InvoiceController : Controller
    {
       
        public IActionResult Index(int Id)
        {
            if (Id == 0)
                return NotFound();
            else
            {
                if (OrderService.OrderExists(Id))
                {
                    return View();
                }
                else
                    return NotFound();


                
            }
        }    
    }
}
