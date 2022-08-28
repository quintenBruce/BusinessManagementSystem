using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using System.Text;
using System.Text.Json;

namespace InventoryManagementSystem.Services
{
    public class WebApiService
    {
        private readonly HttpClient _httpClient;

        public WebApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://localhost:44350/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Order> GetOrder(int id) => await _httpClient.GetFromJsonAsync<Order>("Orders/GetOrder?id=" + id.ToString());

        public async Task<Order> UpdateOrder(OrderDTO orderDTO)
        {
            var result = await _httpClient.PostAsync("Orders/UpdateOrderGroup2", new StringContent(JsonSerializer.Serialize(orderDTO), Encoding.UTF8, "application/json"));
            var order = await result.Content.ReadAsAsync<Order>();
            return order;

        }

        public async Task<List<Category>> GetCategoriesAsync() => await _httpClient.GetFromJsonAsync<List<Category>>("Categories/GetCategories");
        
        
    }
}