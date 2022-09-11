namespace InventoryManagementSystem.Models
{
    public class PagePostsMonthAPIResponsDatum
    {
        public DateTime CreatedTime { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string Story { get; set; }

        private PagePostsMonthAPIResponsDatum()
        {
            CreatedTime.ToUniversalTime();
        }
    }
}