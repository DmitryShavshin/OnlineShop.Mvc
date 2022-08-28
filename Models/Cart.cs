using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Data;

namespace WebApplication2.Models
{
    public class Cart
    {
        public string CartId { get; set; }  
        public IEnumerable<CartItem> ListCartItems { get; set; }


        private readonly ApplicationDbContext _context;
        public Cart(ApplicationDbContext context)
        {
            _context = context;
        }

        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new Cart(context) { CartId = cartId };
        }

        public void AddToCart(Product product)
        {
            var cartItem = _context.CartItems.SingleOrDefault(
                s => s.Product.ProductId == product.ProductId && s.CartId == CartId);
            if (cartItem == null)
            {
                _context.CartItems.Add(new CartItem
                {
                    CartId = CartId,
                    Product = product,
                    Amount = 1
                });
            }        
            _context.SaveChanges();
        }

        public void Remove(Product product)
        {
            var CartItem = _context.CartItems.SingleOrDefault(
                s => s.Product.ProductId == product.ProductId && s.CartId == CartId);
            if (CartItem != null)
            {
                _context.CartItems.Remove(CartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems
                .Where(i => i.CartId == CartId);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public double CartTotal()
        {
            double total = _context.CartItems
                .Where(i => i.CartId == CartId)
                .Select(i => i.Product.Price * i.Amount).Sum();
            return total;
        }
        public IEnumerable<CartItem> GetCartItems()
        {
            return _context.CartItems
                .Where(c => c.CartId == CartId)
                .Include(p => p.Product);
        }
    }
}
