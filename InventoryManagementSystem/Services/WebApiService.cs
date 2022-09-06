using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<Order> GetOrder(int id) => await _httpClient.GetFromJsonAsync<Order>($"Orders/GetOrder?id={id}");

        public async Task<List<Order>> GetOrdersAsync() => await _httpClient.GetFromJsonAsync<List<Order>>("Orders/GetOrders");

        public async Task<Order> UpdateOrder(OrderDTO orderDTO)
        {
            var result = await _httpClient.PostAsync("Orders/UpdateOrderGroup2", new StringContent(System.Text.Json.JsonSerializer.Serialize(orderDTO), Encoding.UTF8, "application/json"));
            var order = await result.Content.ReadAsAsync<Order>();
            return order;
        }

        public async Task<List<Category>> GetCategoriesAsync() => await _httpClient.GetFromJsonAsync<List<Category>>("Categories/GetCategories");

        public async Task<Category> GetCategoryAsync(int id) => await _httpClient.GetFromJsonAsync<Category>("Categories/GetCategory?id=" + id.ToString());

        public async Task<Order> CreateOrderAsync(OrderDTO orderDTO)
        {
            var result = await _httpClient.PostAsync("Orders/CreateOrder", new StringContent(System.Text.Json.JsonSerializer.Serialize(orderDTO), Encoding.UTF8, "application/json"));

            var addedOrder = await result.Content.ReadAsAsync<Order>();
            return addedOrder;
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

        public async Task<bool> DeletePayment(int id)
        {
            var response = await _httpClient.DeleteAsync($"Payments/DeletePayment/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Payment>> UpdatePayments(List<PaymentDTO> payments)
        {
            var result = await _httpClient.PostAsync("Payments/UpdatePayments", new StringContent(System.Text.Json.JsonSerializer.Serialize(payments), Encoding.UTF8, "application/json"));

            var updatedPayments = await result.Content.ReadAsAsync<List<Payment>>();

            return updatedPayments;
        }

        public async Task<bool> CreatePaymentsAsync(List<CreatePaymentDTO> payments)
        {
            var lskdf = System.Text.Json.JsonSerializer.Serialize(payments);
            var result = await _httpClient.PostAsync("Payments/CreatePayments", new StringContent(System.Text.Json.JsonSerializer.Serialize(payments), Encoding.UTF8, "application/json"));

            return result.IsSuccessStatusCode ? true : false;
        }
    }
}