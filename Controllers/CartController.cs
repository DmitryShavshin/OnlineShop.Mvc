using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.ViewModels;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProduct _product;
        private readonly Cart _cart;


        public CartController(IProduct product, Cart cart)
        {
            _product = product;
            _cart = cart;
        }

        public IActionResult Index()
        {
            var items = _cart.GetCartItems();
            _cart.ListCartItems = items;

            var model = new CartViewModel
            {
                Cart = _cart,
                TotalPrice = _cart.CartTotal()
            };

            return View(model);
        }

        public RedirectToActionResult AddProduct(Guid? id)
        {
            var product = _product.GetProduct(id);
            if (product != null)
                _cart.AddToCart(product);

            return RedirectToAction(nameof(Index));
            
        }

        public RedirectToActionResult Remove(Guid? id)
        {

            var product = _product.GetProduct(id);
            if (product != null)
            {
                _cart.Remove(product);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
