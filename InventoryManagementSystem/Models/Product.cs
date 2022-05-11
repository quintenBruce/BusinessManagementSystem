namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Categorie Category { get; set; }
        public Order Order { get; set; }
        public float Price { get; set; }
        public string Dimensions { get; set; }
    }
}
