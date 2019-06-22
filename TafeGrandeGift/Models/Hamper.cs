using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class Hamper
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public decimal HamperPrice { get; set; }
        public string HamperDetail { get; set; }
        public bool IsRemove { get; set; }

        //Here are for files upload
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public long ContentSize { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<HamperProduct> HamperProducts { get; set; }
        public List<HamperFeedBack> HamperFeedBacks { get; set; }
    }
}
