using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VatsaCart.Models
{
    public interface IItemRepository
    {
        IEnumerable<Items> AllItems { get; }
        IEnumerable<Items> ItemOfTheWeek { get; }
        Items GetItemById(int itemId);
    }
}
