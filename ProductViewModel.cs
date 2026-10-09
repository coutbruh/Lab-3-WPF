using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Article { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Count { get; set; }
        public double Discount { get; set; }
        public string? ImagePath { get; set; }

       
        public double PriceWithDiscount => Math.Round(Price * (1 - Discount / 100.0), 2);

        public bool HasBigDiscount => Discount > 20;

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImagePath))
                    return null;

                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", ImagePath);
                return System.IO.File.Exists(path) ? path : null;
            }
        }
    }

}