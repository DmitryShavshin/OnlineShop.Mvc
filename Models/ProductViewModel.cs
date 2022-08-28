using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; }       
        public Guid CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
