using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.HamperViewModels
{
    public class HamperEditViewModel
    {
        public int HamperId { get; set; }
        [Display(Name ="Hamper Name")]
        public string HamperName { get; set; }
        [Display(Name = "Hamper Price")]
        public decimal HamperPrice { get; set; }
        [Display(Name = "Details")]
        public string HamperDetail { get; set; }
        public IEnumerable<string> CategoryName { get; set; }
        public IEnumerable<Category> CategoriesList { get; set; }
        public IEnumerable<int> ProductId { get; set; }
        public IEnumerable<string> ProductNameList { get; set; }
        public IEnumerable<string> ProductQty { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<Product> ProductList { get; set; }
        public List<int> Selected { get; set; }
    }
}
