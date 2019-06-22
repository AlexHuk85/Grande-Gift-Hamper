using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.HamperViewModels
{
    public class HamperCreateViewModel
    {
        public int HamperId { get; set; }
        [Required]
        public string HamperName { get; set; }
        [Range(0, 999.99)]
        public decimal HamperPrice { get; set; }
        public int ProductId { get; set; }
        public List<int> Selected { get; set; }
        public string HamperDetail { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
        public string ProductCategory { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }
        public int CategoryId { get; set; }

        public IFormFile Upload { get; set; }
    }
}
