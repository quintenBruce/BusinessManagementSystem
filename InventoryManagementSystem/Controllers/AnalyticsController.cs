using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class AnalyticsController : Controller
    {
        public IMemoryCache cache;
        private readonly OrdersContext _context;

        public AnalyticsController(IMemoryCache Cache, OrdersContext context)
        {
            cache = Cache;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> orders = new();
            List<Product> products = new();
            List<Payment> payments = new();
            List<Customer> customers = new();
            List<Category> categories = new();

             orders = _context.Orders.ToList();
                customers = _context.Customers.ToList();
                products = _context.Products.Include(product => product.Order).Include(x => x.Category).ToList();
                payments = _context.Payments.ToList();
                categories = _context.Categories.ToList();
            

            var currentDate = DateTime.Now;
            var currentMonthandYear = currentDate.ToString("MM/yyyy");
            var currentMonth = int.Parse(currentDate.ToString("MM"));
            var currentYear = int.Parse(currentDate.ToString("yyyy"));
            var months = new List<string>();
            var currentDayOfYear = currentDate.DayOfYear;
            months.Add("Jan");
            months.Add("Feb");
            months.Add("Mar");
            months.Add("Apr");
            months.Add("May");
            months.Add("Jun");
            months.Add("Jul");
            months.Add("Aug");
            months.Add("Sep");
            months.Add("Oct");
            months.Add("Nov");
            months.Add("Dec");
            Dictionary<string, int[]> categoryCountDic = new Dictionary<string, int[]>(); //keys: category name. Values: category occurrences in products
            var categoryNames = categories.Select(c => c.Name!.Trim()).ToList();
            categoryNames.ForEach(c => categoryCountDic.Add(c, new int[] { 0, 0 }));
            foreach (var product in products)
            {
                categoryCountDic[product.Category!.Name!.Trim()][0] += 1;
                categoryCountDic[product.Category.Name.Trim()][1] += (int)product.Price!;
            }
            var categoryCountTuple = categoryCountDic.Select(x => new Tuple<string, int[]>(x.Key, x.Value)).ToList();
            var categoryListTuplesSorted = categoryCountTuple.OrderByDescending(x => x.Item2[0]).ToList().Take(8);

            ViewBag.categoryNamesList = categoryListTuplesSorted.Select(x => x.Item1).ToList();
            ViewBag.categoryOrderCount = categoryListTuplesSorted.Select(x => x.Item2[0]).ToList();
            ViewBag.categoryRevenue = categoryListTuplesSorted.Select(x => x.Item2[1]).ToList();

            ViewBag.TotalRevenue = orders.Sum(x => x.Total);
            ViewBag.EstimatedProfit = ViewBag.TotalRevenue * .6;
            ViewBag.UpcomingRevenue = products.Where(product => product.Order!.Status == false).Sum(product => product.Price);

            ViewBag.CurrentMonthRevenue = products.Where(x => x.Order!.Status == true && x.Order.CompletionDate?.ToString("MM/yyyy") == currentMonthandYear).Sum(product => product.Price);
            ViewBag.CurrentYearRevenue = products.Where(x => x.Order!.Status == true && x.Order.CompletionDate?.ToString("yyyy") == currentYear.ToString()).Sum(product => product.Price);

            var PreviousYearRevenueUpToCurrentMonth = orders.Where(x => (x.Status == true) && (x.CompletionDate!.Value.ToString("yyyy") == (currentYear - 1).ToString()) && (x.CompletionDate.Value.DayOfYear <= currentDate.DayOfYear)).ToList().Sum(x => x.Total);

            ViewBag.PreviousYearRevenueUpToCurrentMonth = PreviousYearRevenueUpToCurrentMonth;

            ViewBag.EstimatedYearRevenue = (double)ViewBag.CurrentYearRevenue * ((double)365 / (double)currentDayOfYear);

            var currentMonthIndex = currentMonth;

            int[] monthTotalOrdersPlacedArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            float[] monthRevenueList = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int[] monthPastDueOrderList = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] monthOnTimeOrderList = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] monthTotalOrdersCompleted = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (var order in orders)
            {
                if (int.Parse(order.PlacementDate.ToString("yyyy")) == currentYear || (int.Parse(order.CompletionDate.Value.ToString("yyyy")) == currentYear - 1 && int.Parse(order.CompletionDate.Value.ToString("MM")) > currentMonth))
                {
                    var orderPlacementMonth = int.Parse(order.PlacementDate.ToString("MM"));
                    monthTotalOrdersPlacedArray[orderPlacementMonth - 1] += 1;
                }
            }
            foreach (var order in orders.Where(x => x.Status == true).ToList())
            {
                if (int.Parse(order.CompletionDate.Value.ToString("yyyy")) == currentYear || (int.Parse(order.CompletionDate.Value.ToString("yyyy")) == currentYear - 1 && int.Parse(order.CompletionDate.Value.ToString("MM")) > currentMonth))
                {
                    var orderCompletionMonth = int.Parse(order.CompletionDate.Value.ToString("MM"));
                    monthRevenueList[orderCompletionMonth - 1] += order.Total;

                    var orderCompletionDate = order.CompletionDate.Value.Date;

                    if (DateTime.Compare(orderCompletionDate, order.FulfillmentDate.Date) > 0)
                        monthPastDueOrderList[orderCompletionMonth - 1] += 1;
                    else
                        monthOnTimeOrderList[orderCompletionMonth - 1] += 1;
                }
            }

            var monthTotalOrdersPlacedPart1 = monthTotalOrdersPlacedArray.Take(currentMonthIndex).ToList();
            var monthTotalOrdersPlacedPart2 = monthTotalOrdersPlacedArray.Skip(currentMonthIndex).ToList();
            monthTotalOrdersPlacedPart2.AddRange(monthTotalOrdersPlacedPart1);

            var monthPastDueOrderArrayPart1 = monthPastDueOrderList.Take(currentMonthIndex).ToList();
            var monthPastDueOrderArrayPart2 = monthPastDueOrderList.Skip(currentMonthIndex).ToList();
            monthPastDueOrderArrayPart2.AddRange(monthPastDueOrderArrayPart1);

            var monthOnTimeOrderArrayPart1 = monthOnTimeOrderList.Take(currentMonthIndex).ToList();
            var monthOnTimeOrderArrayPart2 = monthOnTimeOrderList.Skip(currentMonthIndex).ToList();
            monthOnTimeOrderArrayPart2.AddRange(monthOnTimeOrderArrayPart1);

            var monthRevenueArrayPart1 = monthRevenueList.Take(currentMonthIndex).ToList();
            var monthRevenueArrayPart2 = monthRevenueList.Skip(currentMonthIndex).ToList();
            monthRevenueArrayPart2.AddRange(monthRevenueArrayPart1);

            var monthNameListPart1 = months.Take(currentMonthIndex).ToList();
            var monthNameListPart2 = months.Skip(currentMonthIndex).ToList();
            monthNameListPart2.AddRange(monthNameListPart1);

            ViewBag.MonthTotalOrdersCompleted = monthPastDueOrderArrayPart2.Select((x, index) => x + monthOnTimeOrderArrayPart2[index]).ToList();//list of total orders for each month
            ViewBag.MonthList = monthNameListPart2; //list of month abbreviations
            ViewBag.MonthRevenueList = monthRevenueArrayPart2;//list of revenue for each month
            ViewBag.MonthOnTimeOrderList = monthOnTimeOrderArrayPart2; //list of orders completed on time for each month
            ViewBag.MonthPastDueOrderList = monthPastDueOrderArrayPart2; //list of orders completed late for each month
            ViewBag.MonthTotalOrdersPlaced = monthTotalOrdersPlacedPart2; //list of orders placed for each month

            //string baseURL = "https://graph.facebook.com/";

            int[] monthEngagedUsersArray = new int[] { 34, 46, 21, 12, 37, 32, 51, 43, 23, 20, 14, 46 };

            //int[] monthEngagedUsersArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //using (var client = new HttpClient())
            //{
            //    PageEngagedUsersAPIResponse data = new();
            //    client.BaseAddress = new Uri(baseURL);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    var APICallDate = DateTime.Now.Date;

            //    for (int i = 11; i >= 0; i--) //get month engaged users information
            //    {
            //        var since = APICallDate.ToString("yyyy-MM-00");
            //        var until = APICallDate.AddMonths(1).ToString("yyyy-MM-01");
            //        int monthEngagedUsers;

            //        if (cache.TryGetValue(since, out monthEngagedUsers))
            //        {
            //            cache.TryGetValue(since, out monthEngagedUsers);
            //            monthEngagedUsersArray[i] = monthEngagedUsers;//
            //        }

            //        else //if key not in cache
            //        {
            //            HttpResponseMessage response = await client.GetAsync("104366531142816/insights/page_engaged_users?period=day&since=" + since + "&until=" + until + "&access_token=EAAw1pkbXUo4BAGO6XhEJ0y1iFOs9MI9PnthpPgTMLBiJ3cOZAEsuITVwwfw3d1c3X3TYNcHvRlXeeZClh2aBZAxRuMUJGXrk1TnZBEZBcsBO2zuq0OZCZCjObbCR2KoOF9ke5wi4oZCbtmRrzYSKQlP0wIAUr2RZCYdXBZC5iY6ZBWoazlVJOApRyqM");

            //            if (response.IsSuccessStatusCode)
            //            {
            //                string results = response.Content.ReadAsStringAsync().Result;
            //                data = JsonConvert.DeserializeObject<PageEngagedUsersAPIResponse>(results);
            //                monthEngagedUsers = data.ValueSum();
            //                monthEngagedUsersArray[i] = monthEngagedUsers;

            //                string key = since;
            //                int value = monthEngagedUsers;
            //                DateTime expirationDate;

            //                expirationDate = since == DateTime.Now.ToString("yyyy-MM-00") ? DateTime.Now.Date.AddDays(1) : DateTime.Now.Date.AddMonths(i);
            //                var options = new MemoryCacheEntryOptions { AbsoluteExpiration = expirationDate };

            //                cache.Set(key, value, options);
            //            }

            //            else
            //                flag = false;
            //        }
            //        APICallDate = APICallDate.AddMonths(-1);
            //    }

            //}
            ViewBag.MonthEngagedUsers = monthEngagedUsersArray;

            //int[] monthPagePostsArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //var pagePostAPIError = false;
            //using (var client = new HttpClient())
            //{
            //    PagePostsMonthAPIResponse date = new();

            //    client.BaseAddress = new Uri(baseURL);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    bool nextCall = true;
            //    var apiCall = "104366531142816/published_posts?limit=100" +
            //        "&access_token=EAAw1pkbXUo4BAGO6XhEJ0y1iFOs9MI9PnthpPgTMLBiJ3cOZAEsuITVwwfw3d1c3X" +
            //        "3TYNcHvRlXeeZClh2aBZAxRuMUJGXrk1TnZBEZBcsBO2zuq0OZCZCjObbCR2KoOF9ke5wi4oZCbtmRrzYSKQl" +
            //        "P0wIAUr2RZCYdXBZC5iY6ZBWoazlVJOApRyqM";
            //    while (nextCall)
            //    {
            //        try
            //        {
            //            HttpResponseMessage response = await client.GetAsync(apiCall);
            //            if (response.IsSuccessStatusCode)
            //            {
            //                string results = response.Content.ReadAsStringAsync().Result;

            //                PagePostsMonthAPIResponse data = JsonConvert.DeserializeObject<PagePostsMonthAPIResponse>(results);

            //                List<DateTime> CreationDates = new();
            //                var resultsJson = JObject.Parse(results);

            //                foreach (var item in resultsJson.Property("data").ElementAt(0))
            //                {
            //                    var post = item.ToList().ElementAt(0);
            //                    var creationDate = post.ToList().ElementAt(0);
            //                    var lksdf = post.ToList().ElementAt(0).GetType();
            //                    CreationDates.Add((DateTime)creationDate);
            //                }

            //                for (int i = 0; i < 100; i++)
            //                {
            //                    data.Data[i].CreatedTime = CreationDates.ElementAt(i);
            //                }

            //                var loopMonth = DateTime.Now;
            //                for (int i = 11; i >= 0; i--, loopMonth = loopMonth.AddMonths(-1))
            //                {
            //                    var monthPagePosts = 0;
            //                    monthPagePosts += data.MonthOccurances(loopMonth);
            //                    monthPagePostsArray[i] += monthPagePosts;
            //                }

            //                if (data.Data.Any(x => x.CreatedTime.Date <= DateTime.Now.AddMonths(-11).AddDays((DateTime.Now.Day - 1) * -1)))
            //                    break;
            //                else
            //                {
            //                    if (data.CheckNext())
            //                        apiCall = data.GetNext();

            //                    else
            //                        break;

            //                }

            //            }
            //        }

            //        catch
            //        {
            //            pagePostAPIError = true;
            //            break;
            //        }
            //    }

            //}

            //ViewBag.monthPagePosts = monthPagePostsArray;

            ViewBag.PendingRevenue = orders.Where(x => x.Status == false).Sum(x => x.Total);
            ViewBag.PendingProfit = ViewBag.PendingRevenue * .6;

            ViewBag.AverageMonthlyRevenue = monthRevenueArrayPart2.Average();

            return View();
        }

       
    }
}