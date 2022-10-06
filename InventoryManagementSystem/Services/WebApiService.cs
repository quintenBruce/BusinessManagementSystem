using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace InventoryManagementSystem.Services
{
    public class WebApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WebApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            

            _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("WebApiAdress")); 
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "739fbe7b3-574c-james4d89-a129-7de168f001fa7");
        }

        public async Task<Order> GetOrder(int id) => await _httpClient.GetFromJsonAsync<Order>($"Orders/GetOrder?id={id}");

        public async Task<List<Order>> GetOrdersAsync() 
        { 
            var response = await _httpClient.GetAsync("Orders/GetOrders");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<Order>>();
            return new List<Order>();
        }
        

        public async Task<Order> UpdateOrder(OrderDTO orderDTO)
        {
            var result = await _httpClient.PostAsync("Orders/UpdateOrderGroup2", new StringContent(System.Text.Json.JsonSerializer.Serialize(orderDTO), Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<Order>();
            }
   
            return new Order { };
        }

        public async Task<List<Category>> GetCategoriesAsync() => await _httpClient.GetFromJsonAsync<List<Category>>("Categories/GetCategories");

        public async Task<Category> GetCategoryAsync(int id) => await _httpClient.GetFromJsonAsync<Category>("Categories/GetCategory?id=" + id.ToString());

        public async Task<Order> CreateOrderAsync(OrderDTO orderDTO)
        {
            var sdlfkjsdf = System.Text.Json.JsonSerializer.Serialize(orderDTO);
            var result = await _httpClient.PostAsync("Orders/CreateOrder", new StringContent(System.Text.Json.JsonSerializer.Serialize(orderDTO), Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<Order>();
            else 
                return null;
            
        }

        public async Task<bool> DeleteOrder(int id)
        {
       
            var response = await _httpClient.DeleteAsync($"Orders/DeleteOrder?id={id}");
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CompleteOrder(int id)
        {
            var result = await _httpClient.PostAsync($"Orders/CompleteOrder/{id}", null);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> CheckOrderExists(int id)
        {
            var response = await _httpClient.GetAsync($"Orders/CheckOrderExists/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<CalenderDTO>> GetCalenderDTOs() => await _httpClient.GetFromJsonAsync<IEnumerable<CalenderDTO>>("Orders/GetOrdersForCalender");

        public async Task<IEnumerable<Payment>> GetPaymentsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Payments/GetPayments/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Payment>>(content);
            }
            else
                return new List<Payment> { };
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Products/GetProducts/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Product>>(content)!;
            }
            else
                return new List<Product> { };
        }

        public async Task<bool> DeletePayment(int id)
        {
            var response = await _httpClient.DeleteAsync($"Payments/DeletePayment/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"Products/DeleteProduct/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<List<Payment>> UpdatePayments(List<PaymentDTO> payments, int orderId)
        {
            var result = await _httpClient.PostAsync($"Payments/UpdatePayments/{orderId}", new StringContent(System.Text.Json.JsonSerializer.Serialize(payments), Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<List<Payment>>();
            return new List<Payment> { };

            
        }

        public async Task<List<Product>> UpdateProducts(List<ProductDTO> products, int orderId)
        {
            var result = await _httpClient.PostAsync($"Products/UpdateProducts/{orderId}", new StringContent(System.Text.Json.JsonSerializer.Serialize(products), Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode) 
                return await result.Content.ReadAsAsync<List<Product>>();

            return new List<Product> { };
        }

        public async Task<bool> CreatePaymentsAsync(List<CreatePaymentDTO> payments)
        {
            var lskdf = System.Text.Json.JsonSerializer.Serialize(payments);
            var result = await _httpClient.PostAsync("Payments/CreatePayments", new StringContent(System.Text.Json.JsonSerializer.Serialize(payments), Encoding.UTF8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> CreateProductsAsync(List<CreateProductDTO> products)
        {
            var lskdf = System.Text.Json.JsonSerializer.Serialize(products);
            var result = await _httpClient.PostAsync("Products/CreateProducts", new StringContent(System.Text.Json.JsonSerializer.Serialize(products), Encoding.UTF8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public async Task<List<Order>> SearchOrdersByName(string name) => await _httpClient.GetFromJsonAsync<List<Order>>($"Orders/SearchOrdersByName/{name}");
    }
}