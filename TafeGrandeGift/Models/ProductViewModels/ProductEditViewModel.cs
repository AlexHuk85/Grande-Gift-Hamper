using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.ProductViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quatity")]
        public string ProductQty { get; set; }
        [Display(Name = "Category Name")]
        public string ProductCategory { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }
    }
}
