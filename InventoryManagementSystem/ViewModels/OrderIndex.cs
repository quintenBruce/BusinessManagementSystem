using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class OrderIndex
    {
        public OrderGroup orderGroup { get; set; }

        [Display(Name = "Category")]
        public OrderRetrievalModel orderRetrievalModel { get; set; }

        public OrderIndex()
        {
            orderRetrievalModel = new OrderRetrievalModel();
        }

    }
}
