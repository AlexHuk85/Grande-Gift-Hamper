using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public List<OrderHamper> OrderedHamper { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalWithShipping { get; set; }
    }
}
