namespace InventoryManagementSystem.Models
{
    public class PageEngagedUsersAPIResponse
    {
        public PageEngagedUsersAPIResponseDatum[] data { get; set; }

        public int ValueSum()
        {
            return data[0].values.Sum(x => x.value);
        }
    }
}