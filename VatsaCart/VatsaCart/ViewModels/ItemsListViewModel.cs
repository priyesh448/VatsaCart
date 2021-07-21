using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatsaCart.Models;

namespace VatsaCart.ViewModels
{
    public class ItemsListViewModel
    {
        public IEnumerable<Items> Items { get; set; }
        public string CurrentCategory { get; set; }
    }
}
