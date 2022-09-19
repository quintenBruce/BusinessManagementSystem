using Anvil.Client;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private WebApiService _webApiService;

        public InvoiceController(WebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        public async Task<IActionResult> Index(int Id)
        {
            if (await _webApiService.CheckOrderExists(Id))
            {
                var order = await _webApiService.GetOrder(Id);
                InvoiceViewModel invoiceViewModel = new(order);
                return View(invoiceViewModel);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> GetInvoice(InvoiceViewModel invoiceViewModel)
        {
            var restClient = new RestClient("0ktlnqe3MiSzSRypO97rUyg3i1dwDo1t");

            var descriptions = invoiceViewModel.ItemDescriptions;
            descriptions!.AddRange(invoiceViewModel.PaymentDescriptions!);

            var QTYs = invoiceViewModel.ItemQTYs;
            QTYs!.AddRange(invoiceViewModel.PaymentQTYs!);

            var unitCosts = invoiceViewModel.ItemUnitCosts;
            unitCosts!.AddRange(invoiceViewModel.PaymentUnitCosts!);

            var amounts = invoiceViewModel.ItemAmounts;
            amounts!.AddRange(invoiceViewModel.PaymentAmounts!);

            Random r = new();

            // Use Payload.Request objects to create your API call.
            var payload = new Anvil.Payloads.Request.FillPdf
            {
                Title = "My PDF Title",
                TextColor = "#3E3E57",
                Data = new Dictionary<string, object>{
                     {"invoiceDate0", DateTime.Now.ToString("yyyy-MM-dd")},
                     {"invoiceNumber1",  r.Next(1672, 38842)},
                     {"customerId2",  r.Next(1672, 38842)},
                     {"purchaseOrderNumber3",  r.Next(1672, 38842)},
                     {"paymentDueBy4",  invoiceViewModel.PaymentDueBy != null ? invoiceViewModel.PaymentDueBy.Value.Date.ToString("yyyy-MM-dd") : ""},
                     {"yourCompanyName5",  "J.R.Woodworking"},
                     {"yourItemName6",  descriptions.ElementAtOrDefault(0) ?? ""},
                     {"yourItemName27",  descriptions.ElementAtOrDefault(1) ?? ""},
                     {"yourItemName38",  descriptions.ElementAtOrDefault(2) ?? ""},
                     {"yourItemName410", descriptions.ElementAtOrDefault(3) ?? ""},
                     {"yourItemName59",  descriptions.ElementAtOrDefault(4) ?? ""},
                     {"yourItemName611", descriptions.ElementAtOrDefault(5) ?? ""},
                     {"yourItemName712", descriptions.ElementAtOrDefault(6) ?? ""},
                     {"yourItemName813", descriptions.ElementAtOrDefault(7) ?? ""},
                     {"unitCost114",  unitCosts.ElementAtOrDefault(0)},
                     {"unitCost215",  unitCosts.ElementAtOrDefault(1)},
                     {"unitCost316",  unitCosts.ElementAtOrDefault(2)},
                     {"unitCost417",  unitCosts.ElementAtOrDefault(3)},
                     {"unitCost518",  unitCosts.ElementAtOrDefault(4)},
                     {"unitCost619",  unitCosts.ElementAtOrDefault(5)},
                     {"unitCost720",  unitCosts.ElementAtOrDefault(6)},
                     {"unitCost821",  unitCosts.ElementAtOrDefault(7)},
                     {"qtyHrRate122", QTYs.ElementAtOrDefault(0)},
                     {"qtyHrRate223", QTYs.ElementAtOrDefault(1)},
                     {"qtyHrRate326", QTYs.ElementAtOrDefault(4)},
                     {"qtyHrRate424", QTYs.ElementAtOrDefault(2)},
                     {"qtyHrRate525", QTYs.ElementAtOrDefault(3)},
                     {"qtyHrRate628", QTYs.ElementAtOrDefault(6)},
                     {"qtyHrRate727", QTYs.ElementAtOrDefault(5)},
                     {"qtyHrRate829", QTYs.ElementAtOrDefault(7)},
                     {"amount130",  amounts.ElementAtOrDefault(0)},
                     {"amount231",  amounts.ElementAtOrDefault(1)},
                     {"amount332",  amounts.ElementAtOrDefault(2)},
                     {"amount433",  amounts.ElementAtOrDefault(3)},
                     {"amount534",  amounts.ElementAtOrDefault(4)},
                     {"amount635",  amounts.ElementAtOrDefault(5)},
                     {"amount736",  amounts.ElementAtOrDefault(6)},
                     {"amount837",  amounts.ElementAtOrDefault(7)},
                     {"specialNotesAndInstructions38",  invoiceViewModel.SpecialNotes ?? ""},
                     {"addressInFooter39",  "Address in footer"},
                     {"emailInFooter42",  "j.r.woodworking@protonmail.com"},
                     {"websiteInFooter43",  "https://jrwoodworking.azurewebsites.net/"},
                     {"subtotal44",  invoiceViewModel.SubTotal ?? 0},
                     {"discount45",  ""},
                     {"taxRate46",  invoiceViewModel.TaxRate ?? 0},
                     {"tax47",  invoiceViewModel.Tax ?? 0},
                     {"total48",  invoiceViewModel.Total ?? 0},
                     {"billingTo49",  invoiceViewModel.CustomerInformation ?? ""},
                     {"shipToIfDifferent50",  ""},
                     {"billTo51",  "Bill to"},
                     {"shipTo52",  ""},
                     {"comapnyAddress53",  "Lubbock, TX 79424"},
                     {"companyContactDetails54",  ""},
                     {"castdc3522101d8c11edaf86ab2abc9d6f1e", ""},
                     { "phoneInFooter40", new {num = "8062397750", region = "US", baseRegion = "US"} },
                     {"faxInFooter41", new {num = "8062397750", region = "US", baseRegion = "US"} }
                 }
            };

            // This will return a `Stream`
            var stream = await restClient.FillPdf("OpLLuf7ThCKei3xSqhrk", payload);
            byte[] bytes = StreamExtensions.GetBytes(stream);

            return new FileContentResult(bytes, "application/pdf");
        }
    }
}