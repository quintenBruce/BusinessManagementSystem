using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        
        private WebApiService _webApiService;

        public ProductController(WebApiService webApiService)
        {

            _webApiService = webApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> DeleteProduct(int productId, int orderId)
        {
            bool status = await _webApiService.DeleteProduct(productId);

            ViewData["categories"] = await _webApiService.GetCategoriesAsync();
            ViewData["orderId"] = orderId;

            var allProducts = await _webApiService.GetProductsAsync(orderId);

            return PartialView("~/Views/Order/_ProductsPartial.cshtml", allProducts);
        }

        [HttpPost]
        public async Task<PartialViewResult> CreateProduct(List<CreateProductDTO> products, int orderId)
        {
            products.ForEach(x => x.OrderId = orderId);
            var productCreateStatus = await _webApiService.CreateProductsAsync(products);

            ViewData["categories"] = await _webApiService.GetCategoriesAsync();
            ViewData["orderId"] = orderId;

            var allProducts = await _webApiService.GetProductsAsync(orderId);
            return PartialView("~/Views/Order/_ProductsPartial.cshtml", allProducts);
        }

        [HttpPost]
        public async Task<PartialViewResult> UpdateProducts(List<ProductDTO> products, int orderId)
        {
            var updatedProducts = await _webApiService.UpdateProducts(products, orderId);

            ViewData["categories"] = await _webApiService.GetCategoriesAsync();
            ViewData["orderId"] = orderId;

            return PartialView("~/Views/Order/_ProductsPartial.cshtml", updatedProducts);
        }
    }
}