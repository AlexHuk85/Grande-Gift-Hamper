using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class OrderHamper
    {
        public int OrderHamperId { get; set; }
        public int OrderId { get; set; }
        public string HamperName { get; set; }
        public int Qty { get; set; }

        public Order Order { get; set; }
        public Hamper Hamper { get; set; }
    }
}
