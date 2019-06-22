using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quatity")]
        public string ProductQty { get; set; }
        //public string ProductDetail { get; set; }
        [Display(Name = "Category Name")]
        public string ProductCategory { get; set; }

        public List<HamperProduct> HamperProducts { get; set; }
        public List<ProductFeedback> ProductFeedBacks { get; set; }
    }
}
