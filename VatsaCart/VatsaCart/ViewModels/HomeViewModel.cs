using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatsaCart.Models;

namespace VatsaCart.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Items> ItemOfTheWeek { get; set; }
    }
}
