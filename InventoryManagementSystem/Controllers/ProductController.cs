﻿using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _productService;

        public ProductController(IProduct productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult DeleteProduct(int productId)
        {
            using (OrdersContext context = new OrdersContext())
            {
                int orderId = context.Products.Include(product => product.Order).Where(product => product.Id == productId).First().Order.Id;

                _productService.DeleteProduct(productId);

                List<Category> categories = new List<Category>();
                categories = context.Categories.ToList();
                ViewData["categories"] = categories;
                ViewData["orderId"] = orderId;

                var allProducts = context.Products.Include(product => product.Order).Include(product => product.Category).Where(product => product.Order.Id == orderId).ToList();

                return PartialView("~/Views/Order/_ProductsPartial.cshtml", allProducts);
            }
        }

        [HttpPost]
        public PartialViewResult CreateProduct(List<Product> products, int orderId)
        {
            using (OrdersContext context = new OrdersContext())
            {
                foreach (var product in products)
                    _productService.CreateProduct(product, orderId);

                List<Category> categories = new List<Category>();
                categories = context.Categories.ToList();
                ViewData["categories"] = categories;
                ViewData["orderId"] = orderId;

                var allProducts = context.Products.Include(product => product.Category).Include(product => product.Order).Where(product => product.Order.Id == orderId).ToList();
                return PartialView("~/Views/Order/_ProductsPartial.cshtml", allProducts);
            }
        }

        [HttpPost]
        public PartialViewResult UpdateProducts(List<Product> products)
        {
            using (OrdersContext context = new OrdersContext())
            {
                var orderId = products[0].Order.Id;
                foreach (var product in products)
                {
                    var existingProductId = product.Id;

                    _productService.UpdateProduct(existingProductId, product);
                }

                var allProducts = context.Products.Include(product => product.Category).Include(product => product.Order).Where(product => product.Order.Id == orderId).ToList();

                List<Category> categories = new List<Category>();
                categories = context.Categories.ToList();
                ViewData["categories"] = categories;
                ViewData["orderId"] = orderId;

                return PartialView("~/Views/Order/_ProductsPartial.cshtml", allProducts);
            }
        }
    }
}