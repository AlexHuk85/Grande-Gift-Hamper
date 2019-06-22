using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TafeGrandeGift.Models.ManageViewModels
{
    public class UserAddressViewModel
    {
        public int UserID { get; set; }
        public List<UserAddress> UserAddressList { get; set; }
        public string NewAddress { get; set; }

        public string SelectedAddress { get; set; }
    }
}
