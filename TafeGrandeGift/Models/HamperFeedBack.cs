using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models
{
    public class HamperFeedBack
    {
        public int HamperFeedBackId { get; set; }
        public string UserName { get; set; }
        public string UserFeedBack { get; set; }
        public int HamperId { get; set; }

        public Hamper Hamper { get; set; }
    }
}
