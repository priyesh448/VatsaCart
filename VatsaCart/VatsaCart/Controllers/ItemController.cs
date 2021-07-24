using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatsaCart.Models;
using VatsaCart.ViewModels;

namespace VatsaCart.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemController(IItemRepository itemRepository,ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        //public IActionResult ListOfAllItems()
        //{
        //    //ViewBag.CurrentCategory = "Sports";
        //    //return View(_itemRepository.AllItems);------way1

        //    //using ViewModel now to pass data to view from controller
        //    ItemsListViewModel itemsListViewModel = new ItemsListViewModel();
        //    itemsListViewModel.Items = _itemRepository.AllItems;
        //    itemsListViewModel.CurrentCategory = "TestDataPass";
        //    return View(itemsListViewModel);
        //}

        public ViewResult ListOfAllItems(string category)
        {
            IEnumerable<Items> items;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                items = _itemRepository.AllItems.OrderBy(p => p.ItemId);
                currentCategory = "All Items";
            }
            else
            {
                items = _itemRepository.AllItems.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.ItemId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new ItemsListViewModel
            {
                Items = items,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            var item = _itemRepository.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
    }
}
