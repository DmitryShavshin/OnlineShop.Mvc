using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class OrderDetailsViewModel
    {
        public IEnumerable<Order> Order { get; set; }
    }
}
