using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Brand
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? BrandImgUrl { get; set; } = null;
        public bool IsChecked { get; set; }
        //[NotMapped]
        public IEnumerable<Product> Products { get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile TitleImg { get; set; }
    }
}
