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
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string Title { get; set; }

        public string? ImgUrl { get; set; }

       
        [ForeignKey("Brand")]
        //[NotMapped]
        public Guid? BrandId { get; set; }
        //[NotMapped]
        public Brand? Brand { get; set; }
        //[NotMapped]
        public IEnumerable<CategoryProduct> CategoryProducts { get; set; }
        //[NotMapped]
        public IEnumerable<ProductImage> ProductImages { get; set; }


        [NotMapped]
        [ValidateNever]
        public IFormFile TitleImg { get; set; }

        [NotMapped]
        [ValidateNever]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
