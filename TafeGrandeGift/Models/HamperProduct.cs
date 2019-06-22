using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class HamperProduct
    {
        public int HamperProductId { get; set; }

        public int HamperId { get; set; }
        public int ProductId { get; set; }
        public Hamper Hamper { get; set; }
        public Product Product { get; set; }
    }
}
