using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class CreateProductViewModel
    {   
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public IFormFile TitleImg { get; set; }
        [Required]
        public IEnumerable<IFormFile> Images { get; set; }
        [Required]
        public Guid BrandId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
