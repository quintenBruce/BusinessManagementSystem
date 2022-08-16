using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Anvil.Client;
using Anvil.Payloads.Request.Types;
using Anvil.Payloads.Response;
using System.Text;
using NPOI.OpenXml4Net.OPC;

namespace InventoryManagementSystem.Controllers
{
    public class ToolsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            
            ViewBag.Response = false;
            var restClient = new RestClient("0ktlnqe3MiSzSRypO97rUyg3i1dwDo1t");




            // Use Payload.Request objects to create your API call.
            var payload = new Anvil.Payloads.Request.FillPdf
            {
                Title = "My PDF Title",
                TextColor = "#3E3E57",
                Data = new Dictionary<string, object>
{


    {"invoiceDate0", "12/94/2023"},
    {"invoiceNumber1",  12345},
    {"customerId2",  12345},
    {"purchaseOrderNumber3",  12345},
    {"paymentDueBy4",  "12/94/2023"},
    {"yourCompanyName5",  "J.R.Woodworking"},
    {"yourItemName6",  "Barndoor Console with stained legs and 32in top withsealed legs and everything else ther eis to know about this product sealed legs and everything else ther eis to know about this product"},
    {"yourItemName27",  "Your item name 2"},
    {"yourItemName38",  "Your item name 3"},
    {"yourItemName59",  "Your item name 5"},
    {"yourItemName410",  "Your item name 4"},
    {"yourItemName712",  "Your item name 7"},
    {"yourItemName813",  "Your item name 8" },
    {"unitCost114",  -1234},
    {"unitCost215",  12345},
    {"unitCost316",  12},
    {"unitCost417",  125.67},
    {"unitCost518",  15.67},
    {"unitCost619",  -245.67},
    {"unitCost720",  12345.7},
    {"unitCost821",  -12345.67},
    {"qtyHrRate122",  12345},
    {"qtyHrRate223",  1245},
    {"qtyHrRate424",  -145},
    {"qtyHrRate525",  1232245},
    {"qtyHrRate326",  125},
    {"qtyHrRate727",  1235},
    {"qtyHrRate628",  145},
    {"qtyHrRate829",  12345},
    {"amount130",  15.67},
    {"amount231",  1.67},
    {"amount332",  1235.7},
    {"amount433",  12345.67},
    {"amount534",  1237},
    {"amount635",  -12345.67},
    {"amount736",  12347},
    {"amount837",  -2345.67},
    {"specialNotesAndInstructions38",  "Special notes and instructions"},
    {"addressInFooter39",  "Address in footer"},
    {"emailInFooter42",  "j.r.woodworking@protonmail.com"},
    {"websiteInFooter43",  "https://jrwoodworking.azurewebsites.net/"},
    {"subtotal44",  12345.67},
    {"discount45",  "Discount"},
    {"taxRate46",  50.3},
    {"tax47",  12345.67},
    {"total48",  12345.67},
    {"billingTo49",  "Billing to"},
    {"shipToIfDifferent50",  ""},
    {"billTo51",  "Bill to"},
    {"shipTo52",  "Ship to"},
    {"comapnyAddress53",  "Lubbock, TX 79424"},
    {"companyContactDetails54",  ""},
    {"yourItemName611",  "Your item name 6"},
    {"castdc3522101d8c11edaf86ab2abc9d6f1e", ""},
    { "phoneInFooter40", new {num = "5554443333", region = "US", baseRegion = "US"} },
    {"faxInFooter41", new {num = "5554443333", region = "US", baseRegion = "US"} }

    }
            };

            // This will return a `Stream`
            var yes = await restClient.FillPdf("OpLLuf7ThCKei3xSqhrk", payload);
            byte[] bytes = StreamExtensions.GetBytes(yes);





            // This will write directly to the path you specify
            

            return new FileContentResult(bytes, "application/pdf");
        }










        [HttpPost]
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

        public async Task<int> Indexi()
        {
            
            return 3;
        }
    }


}
