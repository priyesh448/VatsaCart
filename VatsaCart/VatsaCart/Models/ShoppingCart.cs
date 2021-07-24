using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VatsaCart.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            //IHttpContextAccessor bring a support for session,through IHttpContextAccessor we will get support for HttpContect.Session
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            //check if we have already have sessionid/cartid,if not create a new one.
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            //finally created a shoppingCart with with shoppingcartid with cartid
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        //adding item to cart with amount
        public void AddToCart(Items items, int amount)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Items.ItemId == items.ItemId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Items = items,
                    Amount = 1
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        //remove items from cart
        public int RemoveFromCart(Items Items)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Items.ItemId == Items.ItemId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        //check if items were already were already previously retrieved in current instance
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Items)
                           .ToList());
        }

        //remove all items from cart
        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        //calculates the sum of all the items 
        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Items.Price * c.Amount).Sum();
            return total;
        }
    }
}
