using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication2.ViewModels;
using WebApplication2.Services;

namespace WebApplication2.Reposytory
{
    public class OrderRepository : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;
        private readonly IEmailService _emailService;
        public OrderRepository(ApplicationDbContext context, Cart cart, IEmailService emailService)
        {
            _context = context;
            _cart = cart;
            _emailService = emailService;
        }

        public void CreateOrder(CheckoutViewModel model, ApplicationUser user)
        {
            var cartItems = _cart.ListCartItems;
            var order = new Order()
            {
                OrderId = Guid.NewGuid(),
                UserId = user.Id,
                OrderDate = DateTime.Now,
                FirstName = model.Name,
                LastName = user.LastName,
                TotalPrice = _cart.CartTotal()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
           
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.ProductId,
                    OrderId = order.OrderId,
                    Price = item.Product.Price,
                    UserId = user.Id
                };
                _context.OrderDetails.Add(orderDetail);
            }

            EmailSender emailSender = new EmailSender()
            {
                To = user.Email,
                Subject = "We received your purchase request",
                Body = "we'll be in touch shortly!"
            };
            _emailService.SendEmail(emailSender);
                
            _context.SaveChanges();
        }

        public async Task<IEnumerable<OrderDetail>> GetAllUserOrders(Guid id)
        {
            var orderDetails = await _context.OrderDetails
                                .Where(o => o.UserId == id)
                                .Include(p => p.Product)
                                .Include(o => o.Order)
                                .ToListAsync();

            return orderDetails;
        }
        public async Task<IEnumerable<Order>> GetUserOrders(Guid Id)
        {
            string userId = Id.ToString();
            var orders = await _context.Orders
                                .Include(d => d.OrderDetails)
                                .ThenInclude(p => p.Product)
                                .Where(u => u.UserId == Id)
                                .ToListAsync();
            return orders;
        }

        public Task<Order> GetOrders(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
