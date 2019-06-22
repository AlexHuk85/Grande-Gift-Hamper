using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.ProductViewModels
{
    public class ProductCreateViewModel
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quatity")]
        public string ProductQty { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
    }
}
