using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class OrderController : Controller
    {
        private readonly Cart _cart;
        private readonly IUser _User;
        private readonly IOrder _order;

        public OrderController(Cart cart, IUser User, IOrder order)
        {
            _cart = cart;
            _User = User;
            _order = order; 
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var items = _cart.GetCartItems();
            _cart.ListCartItems = items;

            var userName = User.Identity.Name;
            if (userName == null)
                return RedirectToAction("Index", "User", new { area = "Account" });

            var user = _User.GetUserByName(userName);
            if (user == null)
                return RedirectToAction("Index", "User", new { area = "Account" });

            var model = new CheckoutViewModel()
            {
                Cart = _cart,
                TotalPrice = _cart.CartTotal()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var items = _cart.GetCartItems();
                _cart.ListCartItems = items;
                var viewModel = new CheckoutViewModel()
                {
                    Cart = _cart,
                    TotalPrice = _cart.CartTotal()
                };
                return View(viewModel);
            }

            var userName = User.Identity.Name;
            if (userName == null)
                return RedirectToAction("Index", "User", new { area = "Account" });

            var user = _User.GetUserByName(userName);
            if (user == null)
                return RedirectToAction("Index", "User", new { area = "Account" });

            var cartItems = _cart.GetCartItems();
            _cart.ListCartItems = cartItems;

            _order.CreateOrder(model, user);
            _cart.ClearCart();
            return RedirectToAction("Index", "Home");
        }

    }
}
