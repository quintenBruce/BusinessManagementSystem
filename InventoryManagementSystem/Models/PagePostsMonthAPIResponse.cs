namespace InventoryManagementSystem.Models
{
    public class PagePostsMonthAPIResponse
    {
        public PagePostsMonthAPIResponsDatum[] Data { get; set; }
        public PagePostsMonthAPIResponsePaging Paging { get; set; }

        public int MonthOccurances(DateTime date)
        {
            var occurances = Data.Count(x => x.CreatedTime.Month == date.Month && x.CreatedTime.Year == date.Year);
            return occurances;
        }

        public bool CheckNext()
        {
            return Paging.Next != null ? true : false;
        }

        public string GetNext()
        {
            if (Paging.Next != null)
                return Paging.Next.Replace("https://graph.facebook.com/v14.0/", "");
            else
                return string.Empty;
        }
    }
}