using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class ProductFeedback
    {
        public int ProductFeedbackID { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
