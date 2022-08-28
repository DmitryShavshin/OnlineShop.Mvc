using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Interfaces
{
    public interface IOrder 
    {
        public void CreateOrder(CheckoutViewModel model, ApplicationUser user);
        //public Task<IEnumerable<OrderDetail>> GetAllUserOrders(Guid id);
        public Task<IEnumerable<OrderDetail>> GetAllUserOrders(Guid id);
        public Task<IEnumerable<Order>> GetUserOrders(Guid id);
        public Task<Order> GetOrders(Guid Id);
        
    }
}
