using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VatsaCart.Models
{
    public class ItemsRepository : IItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public ItemsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

       public IEnumerable<Items> AllItems
        {
            get
            {
                return _appDbContext.Items.Include(c => c.Category);
            }
            
        }

        public IEnumerable<Items> ItemOfTheWeek
        {
            get
            {
                return _appDbContext.Items.Include(c => c.Category).Where(p => p.IsItemOfTheWeek);
            }
        }

        public Items GetItemById(int itemId)
        {
            return _appDbContext.Items.FirstOrDefault(p => p.ItemId == itemId);
        }
    }
}
