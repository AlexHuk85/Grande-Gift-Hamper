using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.HamperViewModels
{
    public class HamperDetailViewModel
    {
        public Hamper Hamper { get; set; }
        public IEnumerable<HamperProduct> HamperProducts { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<int> ProductId { get; set; }

        [Display(Name = "Include:")]
        public IEnumerable<string> ProductName { get; set; }
        public bool IsRemove { get; set; }

        public int HamperId { get; set; }
        [Display(Name = "Product Name")]
        public string HamperName { get; set; }

        [Display(Name = "Price")]
        public decimal HamperPrice { get; set; }
        public List<string> ProductNameList { get; set; }

        public string Category { get; set; }
        public string HamperDetail { get; set; }

        [Display(Name = "Image")]
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }

        public IEnumerable<HamperFeedBack> UserFeedBack { get; set; }

        public string UserName { get; set; }
        public string UserFeedback { get; set; }
    }
}
