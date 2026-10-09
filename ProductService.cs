using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Models;
using Microsoft.EntityFrameworkCore;

namespace WpfApp2
{
    public class ProductService
    {
        public List<ProductViewModel> LoadProducts()
        {
            using var ctx = new Lab3ShopContext();

            return ctx.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.Name) 
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Article = p.Article ?? "",
                    CategoryName = p.Category != null ? p.Category.Name ?? "" : "",
                    Description = p.Description ?? "",
                    ManufacturerName = p.Manufacturer != null ? p.Manufacturer.Name ?? "" : "",
                    SupplierName = p.Supplier != null ? p.Supplier.Name ?? "" : "",
                    Price = p.Price ?? 0,
                    Count = p.Count ?? 0,
                    Discount = p.Discount ?? 0,
                    ImagePath = p.ImagePath
                })
                .ToList();
        }
    }
}