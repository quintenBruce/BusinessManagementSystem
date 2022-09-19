using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Utilities
{
    [AttributeUsage(AttributeTargets.All | AttributeTargets.Field, AllowMultiple = false)]
    public class CreateOrderProductsValidationAttribute : ValidationAttribute
    {
        sealed public override bool IsValid(object? value)
        {
            if (value == null)
                return true;
            foreach (var item in (IEnumerable<Product>)value!)
            {
                if (item.Price <= 0 || item.Name.Trim() == null || item.Category!.Name!.Trim() == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
