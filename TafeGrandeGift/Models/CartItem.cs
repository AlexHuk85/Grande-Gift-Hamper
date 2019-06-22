using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class CartItem
    {
        public string CartItemId { get; set; }

        //public string CartId { get; set; }

        public int Quantity { get; set; }

        //public System.DateTime DateCreated { get; set; }

        //public int HamperId { get; set; }

        public Hamper Hamper { get; set; }


    }
}
