using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.HamperViewModels
{
    public class HamperIndexViewModel
    {
        public IEnumerable<int> HamperId { get; set; }
        [Display(Name ="Hamper Name")]
        public IEnumerable<string> HamperName { get; set; }
        [Display(Name = "Hamper Price")]
        public IEnumerable<decimal> HamperPrice { get; set; }
        public bool IsRemove { get; set; }

        [Display(Name = "Image")]
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }

        public List<int> CategoryId { get; set; }
        [Display(Name = "Category")]
        public List<string> CategoryName { get; set; }


        public List<Hamper> Hampers { get; set; }
        public List<Category> Categories { get; set; }
        public SelectList CatogoryForSelect { get; set; }
        public string SearchCategory { get; set; }
        public string SearchInput { get; set; }
        public List<Hamper> DisplayHamper { get; set; }

        public string MaxPrice { get; set; }
        public string MinPrice { get; set; }
    }
}
