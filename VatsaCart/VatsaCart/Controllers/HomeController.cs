using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatsaCart.Models;
using VatsaCart.ViewModels;

namespace VatsaCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemRepository _itemRepository;
     
        public HomeController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
           
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                ItemOfTheWeek = _itemRepository.ItemOfTheWeek
            };

            return View(homeViewModel);
        }
    }
}
