using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VatsaCart.Models
{
    public class MockCategoryRepository:ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
            new List<Category>
            {
            new Category { CategoryId = 1,CategoryName="Grocery",Description="All grocery items"},
            new Category { CategoryId = 2,CategoryName="Cookies",Description="All grocery items"}
            };
}
}
