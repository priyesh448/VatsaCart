﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VatsaCart.Models;
using VatsaCart.ViewModels;

namespace VatsaCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IItemRepository itemRepository, ShoppingCart shoppingCart)
        {
            _itemRepository = itemRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int itemId)
        {
            var selectedItem = _itemRepository.AllItems.FirstOrDefault(p => p.ItemId == itemId);//yha problem hai

            if (selectedItem != null)
            {
                _shoppingCart.AddToCart(selectedItem, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int itemId)
        {
            var selectedItem = _itemRepository.AllItems.FirstOrDefault(p => p.ItemId == itemId);

            if (selectedItem != null)
            {
                _shoppingCart.RemoveFromCart(selectedItem);
            }
            return RedirectToAction("Index");
        }
    }
}
