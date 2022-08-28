using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public ApplicationUser User { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; } 
    }
}
