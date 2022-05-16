namespace InventoryManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public float Price { get; set; }
        public DateTime Order_date { get; set; }
        public DateTime Order_fulfillment_date { get; set; }
        public string Com_thread { get; set; }
        public bool Order_status { get; set; }
        public bool Delivery { get; set; }
        public bool Out_Of_Town { get; set; }

    }
}
