using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class ProductImagesViewModel
    {
        public IEnumerable<IFormFile> Images { get; set; }
        public Product Product { get; set; }
    }
}
