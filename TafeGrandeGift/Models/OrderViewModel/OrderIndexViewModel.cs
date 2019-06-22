using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.OrderViewModel
{
    public class OrderIndexViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int AddressId { get; set; }
        public IEnumerable<UserAddress> Addresses { get; set; }
        public int Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalWithShipping { get; set; }

        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderHamper> OrderHampers { get; set; }
    }
}
