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
    public class Category
    {
        [ValidateNever]
        public Guid CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public string? ImgUrl { get; set; }
        [ValidateNever]
        public bool IsChecked { get; set; }

        //[NotMapped]
        [ValidateNever]
        public IEnumerable<CategoryProduct> CategoryProducts { get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile TitleImg { get; set; }
    }
}
