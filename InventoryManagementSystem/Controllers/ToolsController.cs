
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace InventoryManagementSystem.Controllers
{
    public class ToolsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.Response = false;
            return View();
        }

        
        public async Task<PartialViewResult> DeliveryTool(string address, double gasPrice)
        {
            string baseURL = "https://maps.googleapis.com/maps/api/distancematrix/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var query = "json?origins=33.53091556874041,%20-101.96229348385079&destinations=" + address + "&key=AIzaSyCw3HMSfl7k1HeJnNxeWR-YXVlTxXqcMWQ";

                HttpResponseMessage response = await client.GetAsync(query);

                if (response.IsSuccessStatusCode)
                {
                    DistanceMatrixApiResponse data = new();
                    string results = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<DistanceMatrixApiResponse>(results);
                    ViewBag.Distance = DistanceMatrixApiResponse.GetDistance(data);
                    ViewBag.Duration = DistanceMatrixApiResponse.GetDurationAsString(data);
                    ViewBag.SuggestedDeliveryFee = DistanceMatrixApiResponse.GetSuggestedDeliveryFee(ViewBag.Distance);
                    ViewBag.TripCost = DistanceMatrixApiResponse.GetTripCost(ViewBag.Distance, 3);
                    ViewBag.Response = true;
                    ViewBag.Address = address;
                }
            }
            return PartialView("~/Views/Shared/_DeliveryToolPartial.cshtml");
        }
    }
}